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
    private readonly FaceService _faceService;
    private readonly KafkaProducer _kafkaProducer;

    public MoodController(MoodService moodService, KafkaProducer kafkaProducer, FaceService faceService)
    {
        _moodService = moodService;
        _faceService = faceService;
        _kafkaProducer = kafkaProducer;
    }

    [HttpPost("detect")]
    public async Task<IActionResult> DetectMood([FromQuery] string userId, [FromQuery] string? mood = null)
    {
        var moodRequest = _moodService.DetectMood(userId, mood);
        await _kafkaProducer.PublishMoodAsync(moodRequest);
        return Ok(new { message = "Mood detected and sent to Kafka.", mood = moodRequest.Mood });
    }

    [HttpPost("uploadSelfie")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadSelfie([FromForm] UploadSelfieRequest request)
    {
        if (request.Selfie == null || request.Selfie.Length == 0)
            return BadRequest("Invalid image");

        using var stream = request.Selfie.OpenReadStream();
        var mood = await _faceService.DetectMoodAsync(stream);

        if (mood == null)
            return BadRequest("Could not detect mood");

        var moodRequest = new MoodRequest
        {
            UserId = request.UserId,
            Mood = mood
        };

        await _kafkaProducer.PublishMoodAsync(moodRequest);
        return Ok(new { message = "Mood detected from selfie and sent to Kafka", mood });
    }

}
