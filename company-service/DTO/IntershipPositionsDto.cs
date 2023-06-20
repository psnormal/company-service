using System.ComponentModel.DataAnnotations;

namespace company_service.DTO
{
    public class IntershipPositionsDto
    {
        [Required]
        public List<IntershipPositionInfoDto> intershipPositions { get; set; }

        public IntershipPositionsDto(List<IntershipPositionInfoDto> _intershipPositions)
        {
            intershipPositions = _intershipPositions;
        }
    }
}
