using System.Net;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;

namespace Chinook6OData.Controllers;

[Produces("application/json")]
[ODataRouteComponent("api/[controller]")]
public class AlbumController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<AlbumController> _logger;

    public AlbumController(ChinookContext context, ILogger<AlbumController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var albums = _context.Albums;

        return Ok();
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var album = _context.Albums.FirstOrDefault(a => a.Id == id);

         if (album != null)
         {
             return Ok();
         }
         else
         {
             return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
         }
    }
}