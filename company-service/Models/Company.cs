using System.ComponentModel.DataAnnotations;

namespace company_service.Models
{
    public class Company
    {
        [Required]
        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set;}
        public string CompanyContacts { get; set;}
        public string CompanyAddress { get; set;}
    }
}
