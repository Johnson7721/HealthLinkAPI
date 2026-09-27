using System.ComponentModel.DataAnnotations;

namespace HealthLinkAPI.Models;

public sealed record DoctorQuery
{
    public const int DefaultPageSize = 6;
    public const int MaxPageSize = 50;

    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    [Range(1, MaxPageSize)]
    public int PageSize { get; init; } = DefaultPageSize;

    [MaxLength(100)]
    public string? Search { get; init; }

    [MaxLength(100)]
    public string? Specialty { get; init; }
}
