using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class WorkPlaceInfoDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
