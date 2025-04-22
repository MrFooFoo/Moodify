using PlaylistService.Kafka;
using PlaylistService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // URL of your React app
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DI
builder.Services.AddSingleton<PlaylistProvider>();
builder.Services.AddSingleton<UserPlaylistStore>();
builder.Services.AddHostedService<KafkaConsumer>();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
