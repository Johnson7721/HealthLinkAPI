using System.ComponentModel.DataAnnotations;

namespace HealthLinkAPI.Models;

public sealed class CreateConsultationRequest : IValidatableObject
{
    [Required(ErrorMessage = "Select a doctor to book a consultation.")]
    public Guid DoctorId { get; init; }

    [Required(ErrorMessage = "Choose a consultation type.")]
    [RegularExpression("^(Chat|Video)$", ErrorMessage = "Consultation type must be Chat or Video.")]
    public string ConsultationType { get; init; } = string.Empty;

    [Required(ErrorMessage = "Choose a preferred date and time.")]
    public DateTimeOffset? PreferredDateTime { get; init; }

    [Required(ErrorMessage = "Enter a reason for your visit.")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "Reason for visit must be between 3 and 500 characters.")]
    public string ReasonForVisit { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DoctorId == Guid.Empty)
        {
            yield return new ValidationResult("Select a doctor to book a consultation.", [nameof(DoctorId)]);
        }
        if (string.IsNullOrWhiteSpace(ReasonForVisit))
        {
            yield return new ValidationResult("Enter a reason for your visit.", [nameof(ReasonForVisit)]);
        }

        if (PreferredDateTime is { } preferredDateTime && preferredDateTime <= DateTimeOffset.UtcNow)
        {
            yield return new ValidationResult("Preferred date and time must be in the future.", [nameof(PreferredDateTime)]);
        }

        if (PreferredDateTime is { } requestedDateTime && requestedDateTime.Offset != TimeSpan.Zero)
        {
            yield return new ValidationResult("Preferred date and time must be provided as an ISO 8601 UTC value.", [nameof(PreferredDateTime)]);
        }
    }
}
