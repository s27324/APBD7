using WebApplication2.DTO;

namespace WebApplication2.Repositories;

public interface IWarehouseRepository
{ 
    public Task<bool> IdProductExists(int idProduct);
    public Task<bool> IdWarehouseExists(int idWarehouse);
    public Task<int> ProductDateAndAmountInWarehouse(int idProduct, int amount, DateTime createdAt);
    public Task<bool> CheckIfOrderIsFulfilled(int idOrder);
    public Task<bool> IdOrderInProductWarehouse(int idOrder);
    public Task<int> UpdateFulfilledAt(int idOrder);
    public Task<decimal> PriceForProductWarehouse(int idProduct);
    public Task<int> InsertProductWarehouse(ObjectDTO objectDto, int idOrder);
    public Task<int> GetPrimaryKey(ObjectDTO objectDto, int idOrder);
}