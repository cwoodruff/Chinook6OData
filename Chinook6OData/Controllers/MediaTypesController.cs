using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

public class MediaTypesController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<MediaTypesController> _logger;

    public MediaTypesController(ChinookContext context, ILogger<MediaTypesController> logger)
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
    
    [HttpGet("odata/MediaTypes({id})")]
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