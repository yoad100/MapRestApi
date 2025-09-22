using MapRestApi.Repositories;
using MapRestApi.Repositories.Interfaces;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- MongoDB singleton ---
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoUri = Environment.GetEnvironmentVariable("MONGODB_URI")
                   ?? "mongodb://localhost:27017";
    var client = new MongoClient(mongoUri);
    return client.GetDatabase("MapDb");
});

// Register MongoDB repositories
builder.Services.AddScoped<IPolygonRepository, PolygonRepository>();
builder.Services.AddScoped<IObjectRepository, ObjectRepository>();

var app = builder.Build();

app.UseCors("AllowReactDev");

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
