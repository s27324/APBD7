using WebApplication2.DTO;

namespace WebApplication2.Services;

public interface IProcedureService
{
    public Task<int> AddWithProcedure(ObjectDTO objectDto);
}