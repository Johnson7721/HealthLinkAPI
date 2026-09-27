using HealthLinkAPI.Models;
using HealthLinkAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthLinkAPI.Controllers;

[ApiController]
[Route("doctors")]
public sealed class DoctorsController(IHealthLinkStore healthLinkStore) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<DoctorPage>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<DoctorPage> GetDoctors([FromQuery] DoctorQuery query) => Ok(healthLinkStore.GetDoctors(query));
}
