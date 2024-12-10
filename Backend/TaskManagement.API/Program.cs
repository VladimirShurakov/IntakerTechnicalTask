using Microsoft.EntityFrameworkCore;
using TaskManagement.API.DTO;
using TaskManagement.Data;
using TaskManagement.Data.Context;
using Task = TaskManagement.Data.Entities.Task;
using TaskStatus = TaskManagement.Data.Entities.TaskStatus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManagementDb>(opt => opt.UseInMemoryDatabase("TaskManagementDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

// Defining task group actions
var taskActions = app.MapGroup("/tasks");
taskActions.MapGet("/", GetAllAsync);
taskActions.MapPost("/", CreateAsync);
taskActions.MapPut("/{id}", UpdateAsync);

// Defining a get task statuses action
app.MapGet("/taskStatuses", GetAllStatusesAsync);

app.Run();

static async Task<IResult> GetAllAsync(TaskManagementDb db, CancellationToken cancellationToken)
{
    return TypedResults.Ok(await db.Tasks
        .Include(i => i.Status)
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
    var task = new Task
    {
        Name = createTaskDto.Name,
        Description = createTaskDto.Description,
        Status = new TaskStatus {Id = 1, Name = "Not Started"},
        AssignedTo = createTaskDto.AssignedTo
    };
    db.Tasks.Add(task);
    await db.SaveChangesAsync(cancellationToken);
    var viewTaskDto = new ViewTaskDto(task);
    return TypedResults.Created($"/tasks/{viewTaskDto.Id}", viewTaskDto);
}

static async Task<IResult> UpdateAsync(int id, UpdateTaskDto updateTaskDto, TaskManagementDb db,
    CancellationToken cancellationToken)
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
    // If status has been updated and task is Completed - send message
    if (updatedCount == 1 && status.Name.Equals(Constants.TaskStatusName.Completed))
    {
        //TODO: add ServiceBusHandler
    }
    return TypedResults.NoContent();
}