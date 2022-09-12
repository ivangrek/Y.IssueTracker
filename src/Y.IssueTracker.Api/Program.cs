using Microsoft.EntityFrameworkCore;
using Y.IssueTracker;
using Y.IssueTracker.Api.Infrastructure;
using Y.IssueTracker.Api.Infrastructure.Options;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.Configure<EmailServiceOptions>(builder.Configuration.GetSection("EmailService"));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure services
using var serviceScope = app.Services
    .CreateScope();

var dbContext = serviceScope.ServiceProvider
    .GetRequiredService<ApplicationDbContext>();

dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePages(async statusCodeContext =>
{
    statusCodeContext.HttpContext.Response.ContentType = Text.Plain;

    await statusCodeContext.HttpContext.Response
        .WriteAsync($"Status Code Page: {statusCodeContext.HttpContext.Response.StatusCode}");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
