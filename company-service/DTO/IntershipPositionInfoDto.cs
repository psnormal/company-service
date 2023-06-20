using company_service.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class IntershipPositionInfoDto
    {
        [Required]
        public Guid IntershipPositionId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string IntershipPositionName { get; set; }
        public int IntershipPositionCount { get; set; }
    }
}
