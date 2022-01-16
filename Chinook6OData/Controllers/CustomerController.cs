using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

[Produces("application/json")]
[ODataRouteComponent("odata/[controller]")]
[ODataAttributeRouting]
public class CustomerController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<AlbumController> _logger;

    public CustomerController(ChinookContext context, ILogger<AlbumController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var customers = _context.Customers;

        return Ok(customers);
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var customer = _context.Customers.FirstOrDefault(a => a.Id == id);

        if (customer != null)
        {
            return Ok(customer);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}