using CallCenter.Application.AgentBusiness;
using CallCenter.Application.Services;
using CallCenter.Infrastructure.Data;
using CallCenter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<ICallService, CallService>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
 