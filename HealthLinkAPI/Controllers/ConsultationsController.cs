using HealthLinkAPI.Models;
using HealthLinkAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthLinkAPI.Controllers;

[ApiController]
[Route("consultations")]
public sealed class ConsultationsController(IHealthLinkStore healthLinkStore) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<Consultation>>(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<Consultation>> GetConsultations() => Ok(healthLinkStore.GetConsultations());

    [HttpPost]
    [ProducesResponseType<Consultation>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Consultation> CreateConsultation(CreateConsultationRequest bookingRequest)
    {
        var createdConsultation = healthLinkStore.CreateConsultation(bookingRequest);
        if (createdConsultation is null)
        {
            return NotFound(new { message = "The selected doctor or appointment time is no longer available." });
        }

        return CreatedAtAction(nameof(GetConsultations), createdConsultation);
    }
}
