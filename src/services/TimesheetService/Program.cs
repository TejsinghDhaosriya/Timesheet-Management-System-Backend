using Microsoft.EntityFrameworkCore;
using TimesheetService.DBContext;
using TimesheetService.Repository;
using TimesheetService.Services.Implementations;
using TimesheetService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Service to connect with postgresSql
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TimeSheetDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("ProjectDbConnection")));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
