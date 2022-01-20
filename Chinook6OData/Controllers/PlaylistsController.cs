using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

public class PlaylistsController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<PlaylistsController> _logger;

    public PlaylistsController(ChinookContext context, ILogger<PlaylistsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var playlists = _context.Playlists;

        return Ok(playlists);
    }
    
    [HttpGet("odata/Playlists({id})")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var playlist = _context.Playlists.FirstOrDefault(a => a.Id == id);

        if (playlist != null)
        {
            return Ok(playlist);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}