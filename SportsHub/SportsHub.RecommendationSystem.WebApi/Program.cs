using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.Services;
using SportsHub.RecommendationSystem.Services.Database;
using SportsHub.RecommendationSystem.WebApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var t = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<RecommendationsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    o.IncludeXmlComments(xmlPath);
});
builder.Services.AddHostedService<RecommendationHostedService>();
builder.Services.AddScoped<ISportsHubRepository>(o => new SportsHubRepository(builder.Configuration.GetConnectionString("SportsHub")));
builder.Services.AddScoped<IRecommendationsService, RecommendationsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
