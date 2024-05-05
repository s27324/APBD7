using WebApplication2.DTO;

namespace WebApplication2.Repositories;

public interface IProcedureRepository
{
    public Task<int> AddWithProcedure(ObjectDTO objectDto);
}