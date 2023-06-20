using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace company_service.Models
{
    public class IntershipPosition
    {
        [Required]
        [Key]
        public Guid IntershipPositionId { get; set; }
        public Company Company { get; set; }
        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [Required]
        public string IntershipPositionName { get; set; }
        public string IntershipPositionDescription { get; set; }
        public int IntershipPositionCount { get; set; }
    }
}
