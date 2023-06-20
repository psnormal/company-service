using company_service.DTO;
using company_service.Models;
using System.ComponentModel.DataAnnotations;

namespace company_service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCompany(CompanyCreateUpdateDto model)
        {
            var newCompany = new Company
            {
                CompanyName = model.CompanyName,
                CompanyDescription = model.CompanyDescription,
                CompanyAddress = model.CompanyAddress,
                CompanyContacts = model.CompanyContacts
            };

            await _context.Сompanies.AddAsync(newCompany);
            await _context.SaveChangesAsync();

            return newCompany.CompanyId;
        }

        public CompanyCreateUpdateDto GetCompanyInfo(int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            CompanyCreateUpdateDto result = new CompanyCreateUpdateDto
            {
                CompanyName = companyInfo.CompanyName,
                CompanyDescription = companyInfo.CompanyDescription,
                CompanyAddress = companyInfo.CompanyAddress,
                CompanyContacts = companyInfo.CompanyContacts
            };
            return result;
        }

        public AllCompaniesDto GetAllCompanies()
        {
            List<Company> companies = _context.Сompanies.ToList();
            List<CompanyInfoDto> companyInfoDtos = new List<CompanyInfoDto>();
            foreach(var company in companies)
            {
                CompanyInfoDto newCompany = new CompanyInfoDto
                {
                    CompanyName = company.CompanyName,
                    CompanyId = company.CompanyId
                };
                companyInfoDtos.Add(newCompany);
            }

            AllCompaniesDto allCompanies = new AllCompaniesDto(companyInfoDtos);
            return allCompanies;
        }

        public async Task EditCompany(int id, CompanyCreateUpdateDto model)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            companyInfo.CompanyName = model.CompanyName;
            companyInfo.CompanyDescription = model.CompanyDescription;
            companyInfo.CompanyAddress = model.CompanyAddress;
            companyInfo.CompanyContacts = model.CompanyContacts;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            var companyInfo = _context.Сompanies.FirstOrDefault(c => c.CompanyId == id);

            if (companyInfo == null)
            {
                throw new ValidationException("This company does not exist");
            }

            _context.Сompanies.Remove(companyInfo);
            await _context.SaveChangesAsync();
        }
    }
}
