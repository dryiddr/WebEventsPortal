using Hangfire;
using Microsoft.EntityFrameworkCore;
using Middlewares;
using Persistence.Context;
using Serilog;
using ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Logging.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMyAuthentication();
builder.Services.AddMvc();
builder.Services.InstallServicesInAssembly(configuration);
builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApplyAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddServiceProfiler();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCustomExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("ApplyAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/hangfire");
/*app.UseMvc();*/
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var recurringJobManager = serviceProvider.GetService<IRecurringJobManager>();
serviceProvider.AddArticleViewerCleaner(recurringJobManager);
var context = serviceProvider.GetRequiredService<WebPortalDbContext>();
try
{
    await context.Database.MigrateAsync();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
app.Run();