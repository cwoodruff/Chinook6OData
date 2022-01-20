using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

public class CustomersController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ChinookContext context, ILogger<CustomersController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var customers = _context.Customers;

        return Ok(customers);
    }
    
    [HttpGet("odata/Customers({id})")]
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