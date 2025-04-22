using Common.Models;
using Microsoft.AspNetCore.Mvc;
using MoodDetectionService.Kafka;
using MoodDetectionService.Services;

namespace MoodDetectionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoodController : ControllerBase
{
    private readonly MoodService _moodService;
    private readonly KafkaProducer _kafkaProducer;

    public MoodController(MoodService moodService, KafkaProducer kafkaProducer)
    {
        _moodService = moodService;
        _kafkaProducer = kafkaProducer;
    }

    [HttpPost("detect")]
    public async Task<IActionResult> DetectMood([FromQuery] string userId, [FromQuery] string? mood = null)
    {
        var moodRequest = _moodService.DetectMood(userId, mood);
        await _kafkaProducer.PublishMoodAsync(moodRequest);
        return Ok(new { message = "Mood detected and sent to Kafka.", mood = moodRequest.Mood });
    }
}
