using company_service.DTO;

namespace company_service.Services
{
    public interface ICompanyService
    {
        Task<int> CreateCompany(CompanyCreateUpdateDto model);
        CompanyCreateUpdateDto GetCompanyInfo(int id);
        AllCompaniesDto GetAllCompanies();
        Task EditCompany(int id, CompanyCreateUpdateDto model);
        Task DeleteCompany(int id);
    }
}
