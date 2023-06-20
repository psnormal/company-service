using company_service.DTO;
using company_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace company_service.Controllers
{
    [Route("api")]
    [ApiController]
    public class IntershipPositionController : ControllerBase
    {
        private IIntershipPositionService _intershipPositionService;
        public IntershipPositionController(IIntershipPositionService intershipPositionService) 
        { 
            _intershipPositionService = intershipPositionService;
        }

        [HttpPost]
        [Route("intershipPosition/create")]
        public async Task<ActionResult<IntershipPositionPageDto>> CreateIntershipPosition(IntershipPositionCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var positionId = await _intershipPositionService.CreateIntershipPosition(model);
                return GetIntershipPositionInfo(positionId);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This company does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("intershipPosition/info/{id}")]
        public ActionResult<IntershipPositionPageDto> GetIntershipPositionInfo(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _intershipPositionService.GetIntershipPositionPageInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This intership position does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("intershipPositions")]
        public ActionResult<IntershipPositionsDto> GetAllIntershipPositions()
        {
            try
            {
                return _intershipPositionService.GetAllIntershipPositions();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("company/{id}/intershipPositions")]
        public ActionResult<IntershipPositionsDto> GetAllIntershipPositionsByCompany(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _intershipPositionService.GetAllIntershipPositionsByCompany(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This company does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        [Route("intershipPosition/edit/{id}")]
        public async Task<ActionResult<IntershipPositionPageDto>> EditIntershipPosition(Guid id, IntershipPositionCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _intershipPositionService.EditIntershipPosition(id, model);
                return GetIntershipPositionInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This intership position does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("intershipPosition/delete/{id}")]
        public async Task<ActionResult> DeleteIntershipPosition(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _intershipPositionService.DeleteIntershipPosition(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This intership position does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
