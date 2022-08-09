using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Oinky.TrainingAppAPI;
using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Repositories;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using Oinky.TrainingAppAPI.Services;
using Oinky.TrainingAppAPI.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;

//Set Root Path
Environment.SetEnvironmentVariable("ENV_ROOT_PATH", environment.ContentRootPath);

//Configuration
ConfigurationManager configuration = builder.Configuration;
configuration.SetBasePath(Path.Combine(environment.ContentRootPath, "Config"));
//Add Configurations
configuration.AddJsonFile("settings.json", optional: false, reloadOnChange: true); //Main settings
//configuration.AddJsonFile("dbsettings.json", optional: false, reloadOnChange: true); //DB settings
//configuration.AddJsonFile("logsettings.json", optional: true, reloadOnChange: false); //Logger settings

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
});

builder.Services.AddControllers();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Oinky TrainingApp API", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//Add ServiceLayer
//Repos
builder.Services.AddTransient<ISummonerRepo, SummonerFakeRepo>();
builder.Services.AddTransient<IMatchRepo, MatchFakeRepo>();
//Services
//builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ISummonerService, SummonerService>();
builder.Services.AddTransient<IMatchService, MatchService>();
//Add Background Service
builder.Services.AddHostedService<DataFetcherService>();

var app = builder.Build();

//Init RiotClient
RiotClient.Init(builder.Configuration.GetSection("RiotClientSettings").Get<RiotClientSettings>(), app.Services.GetRequiredService<ILogger<RiotClient>>());

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oinky TrainingApp API v1");
    });
}

//app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();