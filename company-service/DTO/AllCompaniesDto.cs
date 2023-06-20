using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class AllCompaniesDto
    {
        [Required]
        public List<CompanyInfoDto> Companies { get; set; }

        public AllCompaniesDto(List<CompanyInfoDto> companies)
        {
            Companies = companies;
        }
    }
}
