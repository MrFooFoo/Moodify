using Confluent.Kafka;
using System.Text.Json;
using Common.Models;
using PlaylistService.Services;

namespace PlaylistService.Kafka;

public class KafkaConsumer : BackgroundService
{
    private readonly string _topic = "mood-topic";
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly PlaylistProvider _playlistProvider;
    private readonly UserPlaylistStore _userPlaylistStore;

    public KafkaConsumer(IConfiguration config, PlaylistProvider playlistProvider, UserPlaylistStore userPlaylistStore)
    {
        _playlistProvider = playlistProvider;
        _userPlaylistStore = userPlaylistStore;

        var consumerConfig = new ConsumerConfig
        {
            GroupId = "playlist-service-group",
            BootstrapServers = config["Kafka:BootstrapServers"] ?? "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);
                    var moodRequest = JsonSerializer.Deserialize<MoodRequest>(result.Message.Value);
                    if (moodRequest != null)
                    {
                        var playlist = _playlistProvider.GetPlaylist(moodRequest.Mood);
                        _userPlaylistStore.SaveUserPlaylist(moodRequest.UserId, playlist);
                        Console.WriteLine($"🎧 Playlist stored for user {moodRequest.UserId} - Mood: {moodRequest.Mood}");
                    }
                }
                catch (OperationCanceledException) { break; }
                catch (Exception ex)
                {
                    Console.WriteLine($"Kafka error: {ex.Message}");
                }
            }

            _consumer.Close();
        }, stoppingToken);
    }
}
