using System.Net;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace Chinook6OData.Controllers;

// [Produces("application/json")]
// [ODataRouteComponent("odata/[controller]")]
[ODataAttributeRouting]

[ApiController]
[Route("odata/[controller]/[action]")]
public class AlbumsController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<AlbumsController> _logger;

    public AlbumsController(ChinookContext context, ILogger<AlbumsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var albums = _context.Albums;

        return Ok(albums);
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var album = _context.Albums.FirstOrDefault(a => a.Id == id);

         if (album != null)
         {
             return Ok(album);
         }
         else
         {
             return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
         }
    }
}