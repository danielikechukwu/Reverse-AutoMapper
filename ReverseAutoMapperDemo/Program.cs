using Microsoft.EntityFrameworkCore;
using ReverseAutoMapperDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper and the mapping profile
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Register the EmployeeDBContext with dependency injection
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReverseAutoMapperDBConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
