using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;
using WebApplication2.Services;

namespace WebApplication2.Controller;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddNewProductWarehouse(ObjectDTO objectDto)
    {
        bool idProductExists = await _warehouseService.IdProductExists(objectDto.IdProduct);

        if (idProductExists == false)
        {
            var message = "Cannot find product with id: " + objectDto.IdProduct;
            return NotFound(message);
        }
        
        bool idWarehouseExists = await _warehouseService.IdWarehouseExists(objectDto.IdWarehouse);

        if (idWarehouseExists == false)
        {
            var message = "Cannot find warehouse with id: " + objectDto.IdWarehouse;
            return NotFound(message);
        }

        int idOrder =
            await _warehouseService.ProductDateAndAmountInWarehouse(objectDto.IdProduct, objectDto.Amount,
                objectDto.CreatedAt);

        if (idOrder == Int32.MaxValue)
        {
            var message = "Cannot find desired order";
            return NotFound(message);
        }

        bool checkIfOrderIsFulfilled = await _warehouseService.CheckIfOrderIsFulfilled(idOrder);
        
        if (checkIfOrderIsFulfilled == false)
        {
            var message = "Order with id: " + idOrder + " is fulfilled.";
            return NotFound(message);
        }

        bool idOrderInProductWarehouse = await _warehouseService.IdOrderInProductWarehouse(idOrder);
        if (idOrderInProductWarehouse)
        {
            var message = "Order with id: " + idOrder + " is already completed.";
            return NotFound(message);
        }

        await _warehouseService.UpdateFulfilledAt(idOrder);

        await _warehouseService.InsertProductWarehouse(objectDto, idOrder);

        int primaryKey =
            await _warehouseService.GetPrimaryKey(objectDto, idOrder);

        if (primaryKey == Int32.MaxValue)
        {
            var message = "Cannot find primary key for order with id: " + idOrder;
            return NotFound(message);
        }

        var result = "Primary key for desired order is: " + primaryKey;
        return Ok(result);
    }
}
