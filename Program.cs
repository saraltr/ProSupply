using CSE_325_group_project.Components;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using dotenv.net;
using CSE_325_group_project.Data;
using CSE_325_group_project.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");

// register DbContext with Scoped lifetime - recommended for EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// register services
builder.Services.AddScoped<OrderService>();

// add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
