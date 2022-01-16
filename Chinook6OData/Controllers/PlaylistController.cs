using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

[Produces("application/json")]
[ODataRouteComponent("odata/[controller]")]
[ODataAttributeRouting]
public class PlaylistController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<AlbumController> _logger;

    public PlaylistController(ChinookContext context, ILogger<AlbumController> logger)
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
    
    [HttpGet("{id}")]
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