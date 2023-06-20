using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class IntershipPositionCreateUpdateDto
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string IntershipPositionName { get; set; }
        public string IntershipPositionDescription { get; set; }
        public int IntershipPositionCount { get; set; }
    }
}
