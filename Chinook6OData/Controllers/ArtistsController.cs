using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

public class ArtistsController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<ArtistsController> _logger;

    public ArtistsController(ChinookContext context, ILogger<ArtistsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var artists = _context.Artists;

        return Ok(artists);
    }
    
    [HttpGet("odata/Artists({id})")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var artist = _context.Artists.FirstOrDefault(a => a.Id == id);

        if (artist != null)
        {
            return Ok(artist);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}