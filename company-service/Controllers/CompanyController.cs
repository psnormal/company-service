using company_service.DTO;
using company_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace company_service.Controllers
{
    [Route("api")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Route("company/create")]
        public async Task<ActionResult<CompanyCreateUpdateDto>> CreateCompany(CompanyCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var companyId = await _companyService.CreateCompany(model);
                return GetCompanyInfo(companyId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("company/info/{id}")]
        public ActionResult<CompanyCreateUpdateDto> GetCompanyInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _companyService.GetCompanyInfo(id);
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
        [Route("companies")]
        public ActionResult<AllCompaniesDto> GetAllCompanies()
        {
            try
            {
                return _companyService.GetAllCompanies();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        [Route("company/edit/{id}")]
        public async Task<ActionResult<CompanyCreateUpdateDto>> EditCompany(int id, CompanyCreateUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _companyService.EditCompany(id, model);
                return GetCompanyInfo(id);
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

        [HttpDelete]
        [Route("company/delete/{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _companyService.DeleteCompany(id);
                return Ok();
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
    }
}
