using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;
using WebApplication2.Services;

namespace WebApplication2.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProcedureController : ControllerBase
{
    private IProcedureService _procedureService;

    public ProcedureController(IProcedureService procedureService)
    {
        _procedureService = procedureService;
    }

    [HttpPost]
    public async Task<IActionResult> AddNewProductWarehouse(ObjectDTO objectDto)
    {
        int result = await _procedureService.AddWithProcedure(objectDto);

        if (result == Int32.MaxValue)
        {
            return NotFound("Error when adding to Procedure_Warehouse");
        }

        var message = "Primary key for desired order is: " + result;
        return Ok(message);
    }
}