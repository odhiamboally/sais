using DP.Api.Middleware;
using DP.Api.Utilities;
using DP.Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Identity;
using DP.Application.Utilities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.Text.Json.Serialization;

using System.Text.Json;

using System.Text;
using AspNetCoreRateLimit;
using Scalar.AspNetCore;
using DP.Infrastructure.Utilities;
using System.Net;
using DP.Domain.Entities;
using DP.Persistence.SQLServer.DataContext;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

string corsOpenPolicy = "OpenCORSPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
      name: corsOpenPolicy,
      corsPolicyBuilder => {
          corsPolicyBuilder
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});


builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "SAIS",
            Version = "v1",
            Description = """
                By Development Pathways.
                """,
            Contact = new()
            {
                Name = "API Support",
                Email = "admin@developmentpathways.co.uk",
                Url = new Uri("https://www.developmentpathways.co.uk/")
            }
        };

        return Task.CompletedTask;
    });
});

builder.Services.AddRateLimitingServices();
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;

    });



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapOpenApi().CacheOutput();
app.MapScalarApiReference(options =>
{
    // Fluent API
    options
        .WithTitle("DP")
        .WithSidebar(true);
});

app.UseExceptionHandler((_ => { }));
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(corsOpenPolicy);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSecurityHeaders();
app.UseIpRateLimiting();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/scalar/v1"))
   .ExcludeFromDescription();

app.Run();
