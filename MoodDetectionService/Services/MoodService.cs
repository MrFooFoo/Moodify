using Common.Models;

namespace MoodDetectionService.Services;

public class MoodService
{
    private static readonly string[] PossibleMoods = { "Happy", "Sad", "Calm", "Energetic" };

    public MoodRequest DetectMood(string userId, string? selectedMood = null)
    {
        var mood = selectedMood ?? PossibleMoods[new Random().Next(PossibleMoods.Length)];

        return new MoodRequest
        {
            UserId = userId,
            Mood = mood,
            DetectedAt = DateTime.UtcNow
        };
    }
}
