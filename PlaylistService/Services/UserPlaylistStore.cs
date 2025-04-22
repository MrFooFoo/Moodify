using Common.Models;

namespace PlaylistService.Services;

public class UserPlaylistStore
{
    private readonly Dictionary<string, List<PlaylistItem>> _userPlaylists = new();

    public void SaveUserPlaylist(string userId, List<PlaylistItem> playlist)
    {
        _userPlaylists[userId] = playlist;
    }

    public List<PlaylistItem> GetUserPlaylist(string userId)
    {
        return _userPlaylists.TryGetValue(userId, out var playlist) ? playlist : new();
    }
}
