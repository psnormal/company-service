using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class ApplicationsDto
    {
        [Required]
        public List<ApplicationDto> Applications { get; set; }

        public ApplicationsDto(List<ApplicationDto> applications)
        {
            this.Applications = applications;
        }
    }
}
