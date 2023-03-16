using Microsoft.AspNetCore.Mvc;

namespace TenantService.Controllers
{
    [ApiController]
    [Route("api/v1/organization")]
    public class OrganizationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllOrganizations()
        {
            try
            {
                return Ok("All Organizations");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}