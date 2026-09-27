namespace HealthLinkAPI.Models;

public sealed record Doctor(
    Guid Id,
    string Name,
    string Specialty,
    string Availability,
    string ImageUrl,
    bool IsAvailable = true,
    IReadOnlyList<DateTimeOffset>? AvailableSlots = null);
