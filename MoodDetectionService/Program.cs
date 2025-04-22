using MoodDetectionService.Kafka;
using MoodDetectionService.Services;

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

var kafkaServer = builder.Configuration.GetValue<string>("Kafka:BootstrapServers") ?? "localhost:9092";
builder.Services.AddSingleton(new KafkaProducer(kafkaServer));
builder.Services.AddSingleton<MoodService>();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
