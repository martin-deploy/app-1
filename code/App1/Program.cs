using App1;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MartinPleaseOptions>(builder.Configuration.GetSection("MartinPlease"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/", MartinEndpoints.Hello);
app.MapGet("/quote", MartinEndpoints.Quote);
app.MapGet("/meta", MartinEndpoints.Meta);

app.Run();

public partial class Program
{
}
