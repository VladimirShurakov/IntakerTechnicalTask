using Microsoft.EntityFrameworkCore;
using TaskManagement.API.DTO;
using TaskManagement.Data.Context;
using Task = TaskManagement.Data.Models.Task;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManagementDb>(opt => opt.UseInMemoryDatabase("TaskManagementDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//Defining task management actions
var taskActions = app.MapGroup("/tasks");
taskActions.MapGet("/", GetAllAsync);
taskActions.MapPost("/", CreateAsync);
// taskActions.MapPut("/{id}", UpdateTodo);

app.Run();

static async Task<IResult> GetAllAsync(TaskManagementDb db, CancellationToken cancellation)
{
    return TypedResults.Ok(await db.Tasks.Select(x => new ViewTaskDto(x)).ToListAsync(cancellation));
}

static async Task<IResult> CreateAsync(CreateTaskDto createTaskDto, TaskManagementDb db, CancellationToken cancellation)
{
    var dbTask = new Task
    {
        Name = createTaskDto.Name,
        Description = createTaskDto.Description,
        AssignedTo = createTaskDto.AssignedTo
    };
    db.Tasks.Add(dbTask);
    await db.SaveChangesAsync(cancellation);
    var viewTaskDto = new ViewTaskDto(dbTask);
    return TypedResults.Created($"/tasks/{viewTaskDto.Id}", viewTaskDto);
}