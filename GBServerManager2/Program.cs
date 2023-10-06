using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting.WindowsServices;
using Radzen;
using GameServerManager.Models.Options;
using GameServerManager.Data;
using FluentValidation;
using GameServerManager.Web.Validator;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,
    Args = args
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<DialogService>();
builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("LiteDbOptions"));
builder.Services.AddSingleton<LiteDbContext>();
builder.Services.AddScoped<ApplicationSettingsRepository>();
builder.Services.AddScoped<ApplicationSettingsService>();
builder.Services.AddSingleton<GameServerRepository>();
builder.Services.AddSingleton<GameServerService>();

builder.Services.AddScoped<IValidator<GBServer>, GBServerValidator>();

builder.Host.UseWindowsService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
