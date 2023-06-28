using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class InterviewDto
    {
        [Required]
        public string id { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public string location { get; set; }
    }
}
