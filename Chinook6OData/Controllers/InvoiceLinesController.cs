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
public class InvoiceLinesController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<InvoiceLinesController> _logger;

    public InvoiceLinesController(ChinookContext context, ILogger<InvoiceLinesController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var invoiceLines = _context.InvoiceLines;

        return Ok(invoiceLines);
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var invoiceLine = _context.InvoiceLines.FirstOrDefault(a => a.Id == id);

        if (invoiceLine != null)
        {
            return Ok(invoiceLine);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}