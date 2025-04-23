using Common.Models;

namespace PlaylistService.Services;

public class PlaylistProvider
{
    private static readonly Dictionary<string, List<PlaylistItem>> MoodToPlaylists = new()
    {
        ["Happy"] = new List<PlaylistItem>
        {
            new() { Title = "Feeling Good", Artist = "Michael Bublé", Url = "https://www.youtube.com/watch?v=Edwsf-8F3sI" },
            new() { Title = "Sunshine", Artist = "Avicii", Url = "https://www.youtube.com/watch?v=eO_q3ldG6To" },
            new() { Title = "I'm Good", Artist = "David Guetta & Bebe Rexha", Url = "https://www.youtube.com/watch?v=90RLzVUuXe4" }
        },
        ["Sad"] = new List<PlaylistItem>
        {
            new() { Title = "Misery", Artist = "Melodrama", Url = "https://www.youtube.com/watch?v=NIEUtsQJ7lE", },
            new() { Title = "Set Fire to Rain", Artist = "Adele", Url = "https://www.youtube.com/watch?v=a2giXO6eyuI" },
            new() { Title = "SAD!", Artist = "XXXTENTACION", Url = "https://www.youtube.com/watch?v=pgN-vvVVxMA" }
        },
        ["Chill"] = new List<PlaylistItem>
        {
            new() { Title = "Levitating", Artist = "Dua Lipa", Url = "https://www.youtube.com/watch?v=TUVcZfQe-Kw" },
            new() { Title = "Harleys In Hawaii", Artist = "Katy Perry", Url = "https://www.youtube.com/watch?v=sQEgklEwhSo" },
            new() { Title = "Staryboy", Artist = "Weeknd", Url = "https://www.youtube.com/watch?v=34Na4j8AVgA" }
        },
        ["Energetic"] = new List<PlaylistItem>
        {
            new() { Title = "Pump It", Artist = "Black Eyed Peas", Url = "https://www.youtube.com/watch?v=ZaI2IlHwmgQ" },
            new() { Title = "Spinnin", Artist = "Connor Price & Bens", Url = "https://www.youtube.com/watch?v=UekfKh59KOw" },
            new() { Title = "Remember the Name", Artist = "Font Minor", Url = "https://www.youtube.com/watch?v=VDvr08sCPOc" }
        }
    };

    public List<PlaylistItem> GetPlaylist(string mood)
    {
        return MoodToPlaylists.TryGetValue(mood, out var playlist) ? playlist : new();
    }
}
