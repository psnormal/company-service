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
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string patronym { get; set; }
        public string IntershipPositionName { get; set; }
    }
}
