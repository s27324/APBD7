using WebApplication2.DTO;
using WebApplication2.Repositories;

namespace WebApplication2.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<bool> IdProductExists(int idProduct)
    {
        return await _warehouseRepository.IdProductExists(idProduct);
    }

    public async Task<bool> IdWarehouseExists(int idWarehouse)
    {
        return await _warehouseRepository.IdWarehouseExists(idWarehouse);
    }

    public async Task<int> ProductDateAndAmountInWarehouse(int idProduct, int amount, DateTime createdAt)
    {
        return await _warehouseRepository.ProductDateAndAmountInWarehouse(idProduct, amount, createdAt);
    }

    public async Task<bool> CheckIfOrderIsFulfilled(int idOrder)
    {
        return await _warehouseRepository.CheckIfOrderIsFulfilled(idOrder);
    }

    public async Task<bool> IdOrderInProductWarehouse(int idOrder)
    {
        return await _warehouseRepository.IdOrderInProductWarehouse(idOrder);
    }

    public async Task<int> UpdateFulfilledAt(int idOrder)
    {
        return await _warehouseRepository.UpdateFulfilledAt(idOrder);
    }

    public async Task<int> InsertProductWarehouse(ObjectDTO objectDto, int idOrder)
    {
        return await _warehouseRepository.InsertProductWarehouse(objectDto, idOrder);
    }

    public async Task<int> GetPrimaryKey(ObjectDTO objectDto, int idOrder)
    {
        return await _warehouseRepository.GetPrimaryKey(objectDto, idOrder);
    }
}