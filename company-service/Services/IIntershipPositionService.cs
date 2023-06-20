using company_service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace company_service.Services
{
    public interface IIntershipPositionService
    {
        Task<Guid> CreateIntershipPosition(IntershipPositionCreateUpdateDto model);
        IntershipPositionPageDto GetIntershipPositionPageInfo(Guid id);
        IntershipPositionsDto GetAllIntershipPositions();
        IntershipPositionsDto GetAllIntershipPositionsByCompany(int id);
        Task EditIntershipPosition(Guid id, IntershipPositionCreateUpdateDto model);
        Task DeleteIntershipPosition(Guid id);
    }
}
