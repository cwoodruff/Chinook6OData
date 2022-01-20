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
public class InvoicesController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<InvoicesController> _logger;

    public InvoicesController(ChinookContext context, ILogger<InvoicesController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var invoices = _context.Invoices;

        return Ok(invoices);
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var invoice = _context.Invoices.FirstOrDefault(a => a.Id == id);

        if (invoice != null)
        {
            return Ok(invoice);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}