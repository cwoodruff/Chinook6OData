using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

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
    
    [HttpGet("odata/InvoiceLines({id})")]
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