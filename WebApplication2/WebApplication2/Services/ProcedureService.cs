using WebApplication2.DTO;
using WebApplication2.Repositories;

namespace WebApplication2.Services;

public class ProcedureService : IProcedureService
{
    private readonly IProcedureRepository _procedureRepository;

    public ProcedureService(IProcedureRepository procedureRepository)
    {
        _procedureRepository = procedureRepository;
    }

    public async Task<int> AddWithProcedure(ObjectDTO objectDto)
    {
        return await _procedureRepository.AddWithProcedure(objectDto);
    }
}