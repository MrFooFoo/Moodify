using Microsoft.AspNetCore.Mvc;
using PlaylistService.Services;

namespace PlaylistService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaylistController : ControllerBase
{
    private readonly UserPlaylistStore _store;

    public PlaylistController(UserPlaylistStore store)
    {
        _store = store;
    }

    [HttpGet("{userId}")]
    public IActionResult GetPlaylist(string userId)
    {
        var playlist = _store.GetUserPlaylist(userId);
        return Ok(playlist);
    }
}
