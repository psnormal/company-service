using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class CompanyCreateUpdateDto
    {
        [Required]
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyContacts { get; set; }
        public string CompanyAddress { get; set; }
    }
}
