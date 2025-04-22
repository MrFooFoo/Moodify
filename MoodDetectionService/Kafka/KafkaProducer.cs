using Confluent.Kafka;
using Common.Models;
using System.Text.Json;

namespace MoodDetectionService.Kafka;

public class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic = "mood-topic";

    public KafkaProducer(string bootstrapServers)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishMoodAsync(MoodRequest mood)
    {
        var message = JsonSerializer.Serialize(mood);
        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
    }
}
