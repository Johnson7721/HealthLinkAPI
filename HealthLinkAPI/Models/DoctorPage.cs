namespace HealthLinkAPI.Models;

public sealed record DoctorPage(
    IReadOnlyList<Doctor> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    IReadOnlyList<string> Specialties);

