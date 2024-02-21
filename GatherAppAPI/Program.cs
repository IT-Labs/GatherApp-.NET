global using GatherApp.Services.Impl;
using GatherApp.API;
using GatherApp.API.Filters;
using GatherApp.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.SetUpAzureKeyVault(builder.Configuration);
builder.SetUpAPI();
builder.SetUpDB(builder.Configuration);
builder.SetUpDI();
builder.SetUpAzureBlobStorage(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.AddCustomRoleAuthorizationPolicy();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<SetApiResponseStatusCode>();
    options.Filters.Add<LogActionFilter>();
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.SetupCorsPolicy();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        services.GetService<GatherAppContext>()!.Database.Migrate();
    }
}

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var webHostEnvironment = services.GetService<IWebHostEnvironment>();
    SeedEmailData.Initialize(services, webHostEnvironment);
    SeedRoleData.Initialize(services);
    SeedCountryData.Initialize(services);
}

app.UseMiddleware<GatherApp.API.Middleware.AuthenticationMiddleware>();

app.UseExceptionHandler();

app.UseCors("corsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

app.Run();
