using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

[Produces("application/json")]
[ODataRouteComponent("odata/[controller]")]
[ODataAttributeRouting]
public class TrackController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<AlbumController> _logger;

    public TrackController(ChinookContext context, ILogger<AlbumController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var tracks = _context.Tracks;

        return Ok(tracks);
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var track = _context.Tracks.FirstOrDefault(a => a.Id == id);

        if (track != null)
        {
            return Ok(track);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}