using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Oinky.TrainingAppAPI;
using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using Oinky.TrainingAppAPI.Repositories.MSSQL;
using Oinky.TrainingAppAPI.Services;
using Oinky.TrainingAppAPI.Services.Interfaces;
using Oinky.TrainingAppAPI.Utils;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;

//Set Root Path
Environment.SetEnvironmentVariable(APIUtils.CONTENT_ROOT_PATH_PARAM, environment.ContentRootPath);

//Configuration
ConfigurationManager configuration = builder.Configuration;
configuration.SetBasePath(Path.Combine(environment.ContentRootPath, "Config"));
//Add Configurations
configuration.AddJsonFile("settings.json", optional: false, reloadOnChange: true); //Main settings
configuration.AddJsonFile("dbsettings.json", optional: false, reloadOnChange: true); //DB settings
//configuration.AddJsonFile("logsettings.json", optional: true, reloadOnChange: false); //Logger settings

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
builder.Services.AddTransient<ISummonerRepo, SummonerMSSQLRepo>();
builder.Services.AddTransient<IMatchRepo, MatchMSSQLRepo>();
builder.Services.AddTransient<IGoalRepo, GoalMSSQLRepo>();
//Services
builder.Services.AddTransient<ISummonerService, SummonerService>();
builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddTransient<IGoalService, GoalService>();
builder.Services.AddSingleton<IconService>();
//Add Background Service
builder.Services.AddHostedService<DataFetcherService>();

//CORS
builder.Services.AddCors();

var app = builder.Build();

//Init APIUtils
APIUtils.APISettings = builder.Configuration.GetSection("APISettings").Get<APISettings>();

//Init RiotClient
RiotClient.Init(builder.Configuration.GetSection("RiotClientSettings").Get<RiotClientSettings>(), app.Services.GetRequiredService<ILogger<RiotClient>>());

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Oinky TrainingApp API v1");
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();