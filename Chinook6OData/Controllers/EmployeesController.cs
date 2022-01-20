using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Chinook6OData.Controllers;

public class EmployeesController : ODataController
{
    private readonly ChinookContext _context;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(ChinookContext context, ILogger<EmployeesController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        var employees = _context.Employees;

        return Ok();
    }
    
    [HttpGet("odata/Employees({id})")]
    [EnableQuery]
    public IActionResult Get(int id)
    {
        var employee = _context.Employees.FirstOrDefault(a => a.Id == id);

        if (employee != null)
        {
            return Ok(employee);
        }
        else
        {
            return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");
        }
    }
}