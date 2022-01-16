using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

[Produces("application/json")]
[ODataRouteComponent("odata/[controller]")]
[ODataAttributeRouting]
public class MediaTypeController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<AlbumController> _logger;

    public MediaTypeController(ChinookContext context, ILogger<AlbumController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var mediaTypes = _context.MediaTypes;

        return Ok(mediaTypes);
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var mediaType = _context.MediaTypes.FirstOrDefault(a => a.Id == id);

        if (mediaType != null)
        {
            return Ok(mediaType);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}