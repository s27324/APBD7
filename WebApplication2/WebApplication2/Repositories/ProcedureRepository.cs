using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebApplication2.DTO;

namespace WebApplication2.Repositories;

public class ProcedureRepository : IProcedureRepository
{
    private IConfiguration _configuration;

    public ProcedureRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> AddWithProcedure(ObjectDTO objectDto)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        await con.OpenAsync();

        DbTransaction tran = await con.BeginTransactionAsync();

        try
        {
            await using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = (SqlTransaction)tran;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddProductToWarehouse";
            cmd.Parameters.AddWithValue("@IdProduct", objectDto.IdProduct);
            cmd.Parameters.AddWithValue("@IdWarehouse", objectDto.IdWarehouse);
            cmd.Parameters.AddWithValue("@Amount", objectDto.Amount);
            cmd.Parameters.AddWithValue("@CreatedAt", objectDto.CreatedAt);

            var result = await cmd.ExecuteScalarAsync();
            await tran.CommitAsync();

            return Convert.ToInt32(result);
        }
        catch (Exception e)
        {
            try
            {
                if (tran != null)
                {
                    await tran.RollbackAsync();
                }
            }
            catch (Exception e2)
            {
                
            }
            throw;
        }
    }
}