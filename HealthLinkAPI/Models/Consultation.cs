namespace HealthLinkAPI.Models;

public sealed record Consultation(
    Guid Id,
    Guid DoctorId,
    string DoctorName,
    string Specialty,
    string ConsultationType,
    DateTimeOffset PreferredDateTime,
    string ReasonForVisit,
    string Status,
    string PatientId);
