using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

public class TracksController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<TracksController> _logger;

    public TracksController(ChinookContext context, ILogger<TracksController> logger)
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
    
    [HttpGet("odata/Tracks({id})")]
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