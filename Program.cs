using CSE_325_group_project.Components;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using dotenv.net;
using CSE_325_group_project.Data;
using CSE_325_group_project.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using CSE_325_group_project.Services;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");

builder.Logging.AddConsole();  // add Console logging

// Register DbContext with Scoped lifetime - recommended for EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
        //    .EnableSensitiveDataLogging()  // shows full SQL queries with parameters for dev only
        //    .LogTo(Console.WriteLine, LogLevel.Information)  // logs SQL queries to console for debugging
);

// register services
builder.Services.AddScoped<OrderService>();

// add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// configure HttpClient for API communication
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:5259");
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        //allow invalid SSL certificates. only for local development.
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5259")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();


// Configure Memory Cache
builder.Services.AddMemoryCache();

// add controllers (for APIs)
builder.Services.AddControllers();

// add JWT Bearer authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "yourapp",
        ValidAudience = "yourapp",
    };
});

var app = builder.Build();

// configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

// enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// map API controllers
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
