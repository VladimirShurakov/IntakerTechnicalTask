using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Infrastructure;
using Infrastructure.Messages;
using Infrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.Configuration;
using TaskManagement.API.DTO;
using TaskManagement.API.Extensions;
using TaskManagement.API.Options;
using TaskManagement.Data;
using TaskManagement.Data.Context;

// Minimal WebAPI style
var builder = WebApplication.CreateBuilder(args);
Configure(builder);
var app = builder.Build();

// Defining task group actions
var taskActions = app.MapGroup("/tasks");
taskActions.MapGet("/", GetAllAsync);
taskActions.MapPost("/", CreateAsync);
taskActions.MapPut("/{id:int}", UpdateAsync);
// Defining a get task statuses action
app.MapGet("/taskStatuses", GetAllStatusesAsync);
app.Run();

static void Configure(WebApplicationBuilder builder)
{
    var configuration = builder.Configuration;
    // EF Configuration
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<TaskManagementDb>(opt => opt.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // Service Bus Configuration
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    builder.Services.Configure<ServiceBusOptions>(builder.Configuration.GetSection(nameof(ServiceBusOptions)));
    var serviceBusOptions = configuration.GetSection("ServiceBusOptions").Get<ServiceBusOptions>() ??
                            throw new InvalidOperationException($"Missing {nameof(ServiceBusOptions)} configuration.");
    var clientOptions = new ServiceBusClientOptions
    {
        TransportType = ServiceBusTransportType.AmqpTcp,
        RetryOptions = new ServiceBusRetryOptions
        {
            MaxRetries = serviceBusOptions.RetryCount,
            Delay = TimeSpan.FromMilliseconds(serviceBusOptions.RetryDelayMs)
        }
    };
    if (environment != null && environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
    {
        builder.Services.AddSingleton(_ => new ServiceBusClient(serviceBusOptions.ConnectionOrFqdn, clientOptions));
    }
    else
    {
        builder.Services.AddSingleton(_ =>
            new ServiceBusClient(serviceBusOptions.ConnectionOrFqdn, new DefaultAzureCredential(), clientOptions));
    }
    builder.Services.AddSingleton<IMessageSender, ServiceBusMessageSender>();
    builder.Services.AddScoped(typeof(BrokeredMessageBuilder<>));
}

static async Task<IResult> GetAllAsync(TaskManagementDb db, CancellationToken cancellationToken)
{
    return TypedResults.Ok(await db.Tasks
        .Include(i => i.ProjectTaskStatus)
        .OrderBy(x => x.Name)
        .Select(x => new ViewTaskDto(x))
        .ToListAsync(cancellationToken));
}

static async Task<IResult> GetAllStatusesAsync(TaskManagementDb db, CancellationToken cancellationToken)
{
    return TypedResults.Ok(await db.TaskStatuses
        .OrderBy(x => x.Name)
        .Select(x => new ViewTaskStatusDto(x))
        .ToListAsync(cancellationToken));
}

static async Task<IResult> CreateAsync(CreateTaskDto createTaskDto, TaskManagementDb db,
    CancellationToken cancellationToken)
{
    var notStartedTask = await db.TaskStatuses.FindAsync(Constants.TaskStatusId.NotStarted, cancellationToken);
    if (notStartedTask == null) return TypedResults.BadRequest("Default task status was not found");

    var task = createTaskDto.ToEntity(notStartedTask.Id);
    db.Tasks.Add(task);
    await db.SaveChangesAsync(cancellationToken);
    var viewTaskDto = new ViewTaskDto(task);
    return TypedResults.Created($"/tasks/{viewTaskDto.Id}", viewTaskDto);
}

static async Task<IResult> UpdateAsync(int id, UpdateTaskDto updateTaskDto, TaskManagementDb db,
    IOptions<ServiceBusOptions> serviceBusOptions, IMessageSender serviceBusHandler,
    BrokeredMessageBuilder<TaskCompletedMessage> builder, CancellationToken cancellationToken)
{
    // Checking if a status with such ID exists
    var status = await db.TaskStatuses.FindAsync(updateTaskDto.StatusId, cancellationToken);
    if (status is null) return TypedResults.BadRequest("Status not found");
    // Getting a task by ID
    var task = await db.Tasks.FindAsync(id, cancellationToken);
    if (task is null) return TypedResults.NotFound();
    // Update the status
    task.StatusId = updateTaskDto.StatusId;
    var updatedCount = await db.SaveChangesAsync(cancellationToken);
    // If status has been updated and task is Completed - send the message
    if (updatedCount == 1 && status.Name.Equals(Constants.TaskStatusName.Completed))
    {
        var taskCompletedMessage = new TaskCompletedMessage {TaskId = task.Id, CompletionDate = DateTime.UtcNow};
        var serviceBusMessage = builder.AddBody(taskCompletedMessage)
            .AddTimeToLive(TimeSpan.FromHours(serviceBusOptions.Value.MessageTimeToLiveDays))
            .Build();
        // TODO: I'd better implemented Transactional outbox pattern to avoid any compensatory actions in case of
        // message sending failure
        await serviceBusHandler.SendMessageAsync(serviceBusOptions.Value.QueueOrTopicName, serviceBusMessage,
            cancellationToken);
    }

    return TypedResults.NoContent();
}