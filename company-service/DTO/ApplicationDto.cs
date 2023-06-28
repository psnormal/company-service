using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class ApplicationDto
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string positionId { get; set; }
        [Required]
        public List<string> status { get; set; }
        [Required]
        public List<InterviewDto> interview { get; set; }
        [Required]
        public string studentId { get; set; }
    }
}
