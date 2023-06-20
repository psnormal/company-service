using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class CompanyInfoDto
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
