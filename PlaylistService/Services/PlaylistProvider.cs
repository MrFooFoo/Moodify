using Common.Models;

namespace PlaylistService.Services;

public class PlaylistProvider
{
    private static readonly Dictionary<string, List<PlaylistItem>> MoodToPlaylists = new()
    {
        ["Happy"] = new List<PlaylistItem>
        {
            new() { Title = "Happy Vibes", Artist = "Pharrell", Url = "https://music.example.com/happyvibes" },
            new() { Title = "Sunshine", Artist = "Avicii", Url = "https://music.example.com/sunshine" }
        },
        ["Sad"] = new List<PlaylistItem>
        {
            new() { Title = "Blue Hour", Artist = "Coldplay", Url = "https://music.example.com/bluehour" },
            new() { Title = "Rainy Days", Artist = "Adele", Url = "https://music.example.com/rainydays" }
        },
        ["Calm"] = new List<PlaylistItem>
        {
            new() { Title = "Peaceful Piano", Artist = "Various", Url = "https://music.example.com/peaceful" }
        },
        ["Energetic"] = new List<PlaylistItem>
        {
            new() { Title = "Pump It", Artist = "Black Eyed Peas", Url = "https://music.example.com/pumpit" },
            new() { Title = "Power Hour", Artist = "DJ Mix", Url = "https://music.example.com/powerhour" }
        }
    };

    public List<PlaylistItem> GetPlaylist(string mood)
    {
        return MoodToPlaylists.TryGetValue(mood, out var playlist) ? playlist : new();
    }
}
