using Karma_Backend.Models;
using Karma_Backend.Services;
using Microsoft.EntityFrameworkCore;
using Karma_Backend.Hub;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {                       
            policy.WithOrigins("http://localhost:4200",
            "https://white-beach-015cbaf0f.5.azurestaticapps.net").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserRoomConnection>>(IServiceProvider =>
new Dictionary<string, UserRoomConnection>());

// Add HttpClient for making API requests
builder.Services.AddHttpClient();

// Register DbContext with connection string (example using SQL Server)
builder.Services.AddDbContext<KarmaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the GeocodingService
builder.Services.AddSingleton<GeocodingService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var googleApiKey = builder.Configuration["GoogleApiSettings:ApiKey"];
    return new GeocodingService(httpClientFactory.CreateClient(), googleApiKey);
});

// Register LocationService
builder.Services.AddScoped<LocationService>();
var app = builder.Build();
// Add controllers


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<ChatHub>("/chat");
});

app.MapControllers();

app.Run();
