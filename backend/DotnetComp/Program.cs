using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using DotnetComp.Clients;
using DotnetComp.Data;
using DotnetComp.Repositories;
using DotnetComp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Api versioning
builder
    .Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new HeaderApiVersionReader("X-Api-Version")
        );
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHttpClient(
    "RunescapeClient",
    client => client.BaseAddress = new Uri("https://secure.runescape.com/")
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetComp - V1", Version = "v1.0" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// DI
builder.Services.AddScoped<IRunescapeClient, RunescapeClient>();

builder.Services.AddScoped<IHiscoreService, HiscoreService>();

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

builder.Services.AddScoped<IGroupRepository, GroupRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



// Setup database
var connectionString =
    builder.Configuration.GetConnectionString("sqlite")
    ?? throw new InvalidOperationException("Connection string for database not found.");

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));

//Auth


// if(builder.Environment.IsProduction()) {
//     var githubConfig = builder.Environment.EnvironmentName    
// } else {
//     var githubConfig = builder.Configuration.GetSection("GithubDev");
// }

var githubConfig = builder.Configuration.GetSection("Github") ?? throw new InvalidOperationException("Github config not found");

// Need to set CookieAuthenticationDefaults here otherwise stack overflow see: https://github.com/dotnet/aspnetcore/issues/42975
builder
    .Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.LoginPath = "/auth/login";
        }
    )
    .AddOAuth(
        "github",
        "github",
        options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            options.ClientId =
                githubConfig["ClientId"]
                ?? throw new InvalidOperationException("Client id not found");

            options.ClientSecret =
                githubConfig["ClientSecret"]
                ?? throw new InvalidOperationException("Client secret not found");

            options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
            options.TokenEndpoint = "https://github.com/login/oauth/access_token";

            options.UserInformationEndpoint = "https://api.github.com/user";

            options.CallbackPath = "/oauth/github-cb";

            options.SaveTokens = true;

            options.ClaimActions.MapJsonKey("sub", "id");
            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "login");

            options.Events.OnCreatingTicket = async ctx =>
            {
                using var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    ctx.Options.UserInformationEndpoint
                );

                request.Headers.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    ctx.AccessToken
                );
                using var result = await ctx.Backchannel.SendAsync(request);
                var user = await result.Content.ReadFromJsonAsync<JsonElement>();
                ctx.RunClaimActions(user);
            };
        }
    );

var app = builder.Build();


app.UsePathBase("/.api");

app.Logger.LogInformation("Running in development: {IsDevelopment}", app.Environment.IsDevelopment());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
