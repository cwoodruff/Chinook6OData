using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

// [Produces("application/json")]
// [ODataRouteComponent("odata/[controller]")]
// [ODataAttributeRouting]

[ApiController]
[Route("odata/[controller]/[action]")]
public class GenresController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<GenresController> _logger;

    public GenresController(ChinookContext context, ILogger<GenresController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var genres = _context.Genres;

        return Ok(genres);
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var genre = _context.Genres.FirstOrDefault(a => a.Id == id);

        if (genre != null)
        {
            return Ok(genre);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}