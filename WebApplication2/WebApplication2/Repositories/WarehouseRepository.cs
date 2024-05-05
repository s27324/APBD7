
using System.Data.SqlClient;
using WebApplication2.DTO;

namespace WebApplication2.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> IdProductExists(int idProduct)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM PRODUCT WHERE IdProduct = @idProduct";
        cmd.Parameters.AddWithValue("@idProduct", idProduct);

        if (await cmd.ExecuteScalarAsync() is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> IdWarehouseExists(int idWarehouse)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM WAREHOUSE WHERE IdWarehouse = @idWarehouse";
        cmd.Parameters.AddWithValue("@idWarehouse", idWarehouse);

        if (await cmd.ExecuteScalarAsync() is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<int> ProductDateAndAmountInWarehouse(int idProduct, int amount, DateTime createdAt)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdOrder FROM [ORDER] WHERE IdProduct = @idProduct AND Amount = @amount AND CreatedAt < @createdAt";
        cmd.Parameters.AddWithValue("@idProduct", idProduct);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@createdAt", createdAt);

        var result = await cmd.ExecuteScalarAsync();
        return result is not null ? (int)result : Int32.MaxValue;
    }

    public async Task<bool> CheckIfOrderIsFulfilled(int idOrder)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM [ORDER] WHERE IdOrder = @idOrder AND FulfilledAt is null";
        cmd.Parameters.AddWithValue("@idOrder", idOrder);

        if (await cmd.ExecuteScalarAsync() is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> IdOrderInProductWarehouse(int idOrder)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM PRODUCT_WAREHOUSE WHERE IdOrder = @idOrder";
        cmd.Parameters.AddWithValue("@idOrder", idOrder);

        if (await cmd.ExecuteScalarAsync() is null)
        {
            return false;
        }

        return true;
    }

    public async Task<int> UpdateFulfilledAt(int idOrder)
    {
        var currentDate = DateTime.Now;
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE [Order] SET FulfilledAt=@currentDate WHERE IdOrder = @idOrder";
        cmd.Parameters.AddWithValue("@idOrder", idOrder);
        cmd.Parameters.AddWithValue("@currentDate", currentDate);

        var affectedCount = await cmd.ExecuteNonQueryAsync();
        return affectedCount;
    }

    public async Task<decimal> PriceForProductWarehouse(int idProduct)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT Price FROM PRODUCT WHERE IdProduct=@idProduct";
        cmd.Parameters.AddWithValue("@idProduct", idProduct);

        var result = await cmd.ExecuteScalarAsync();
        return result is not null ? (decimal) result : 0;
    }

    public async Task<int> InsertProductWarehouse(ObjectDTO objectDto, int idOrder)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        var price =  await PriceForProductWarehouse(objectDto.IdProduct);
        price *= objectDto.Amount;
        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO PRODUCT_WAREHOUSE(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@idWarehouse, @idProduct, @idOrder, @amount, @price, @createdAt)";
        cmd.Parameters.AddWithValue("@idWarehouse", objectDto.IdWarehouse);
        cmd.Parameters.AddWithValue("@idProduct", objectDto.IdProduct);
        cmd.Parameters.AddWithValue("@idOrder", idOrder);
        cmd.Parameters.AddWithValue("@amount", objectDto.Amount);
        cmd.Parameters.AddWithValue("@price", price);
        cmd.Parameters.AddWithValue("@createdAt", objectDto.CreatedAt);

        var affectedCount = await cmd.ExecuteNonQueryAsync();
        return affectedCount;
    }

    public async Task<int> GetPrimaryKey(ObjectDTO objectDto, int idOrder)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdProductWarehouse FROM PRODUCT_WAREHOUSE WHERE IdProduct = @idProduct AND IdWarehouse = @idWarehouse AND IdOrder=@idOrder";
        cmd.Parameters.AddWithValue("@idProduct", objectDto.IdProduct);
        cmd.Parameters.AddWithValue("@idWarehouse", objectDto.IdWarehouse);
        cmd.Parameters.AddWithValue("@idOrder", idOrder);

        var result = await cmd.ExecuteScalarAsync();
        return result is not null ? (int)result : Int32.MaxValue;
    }
}