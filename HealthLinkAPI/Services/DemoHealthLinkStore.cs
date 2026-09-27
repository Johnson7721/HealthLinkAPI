using HealthLinkAPI.Models;

namespace HealthLinkAPI.Services;

public sealed class DemoHealthLinkStore : IHealthLinkStore
{
    private const string DemoPatientId = "patient-demo";
    private readonly object _consultationsLock = new();
    private readonly List<Doctor> _availableDoctors;
    private readonly List<Consultation> _patientConsultations;

    public DemoHealthLinkStore()
    {
        var currentDateTime = DateTimeOffset.Now;
        _availableDoctors =
        [
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260101", "Dr. Sarah Lim", "General Practitioner", "https://randomuser.me/api/portraits/women/44.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260102", "Dr. Amir Rashid", "Family Medicine", "https://randomuser.me/api/portraits/men/32.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260103", "Dr. Priya Nair", "Internal Medicine", "https://randomuser.me/api/portraits/women/68.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260104", "Dr. Daniel Tan", "Cardiology", "https://randomuser.me/api/portraits/men/45.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260105", "Dr. Mei Ling Wong", "Dermatology", "https://randomuser.me/api/portraits/women/21.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260106", "Dr. Hafiz Ismail", "General Practitioner", "https://randomuser.me/api/portraits/men/12.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260107", "Dr. Rachel Ooi", "Pediatrics", "https://randomuser.me/api/portraits/women/12.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260108", "Dr. Kumar Selvam", "Family Medicine", "https://randomuser.me/api/portraits/men/75.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260109", "Dr. Elaine Chua", "Internal Medicine", "https://randomuser.me/api/portraits/women/33.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260110", "Dr. Jason Lee", "Cardiology", "https://randomuser.me/api/portraits/men/22.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260111", "Dr. Nurul Huda", "Dermatology", "https://randomuser.me/api/portraits/women/57.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260112", "Dr. Marcus Goh", "General Practitioner", "https://randomuser.me/api/portraits/men/56.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260113", "Dr. Aisha Rahman", "Pediatrics", "https://randomuser.me/api/portraits/women/81.jpg", currentDateTime),
            CreateDoctor("7fb7f3e7-87c0-4f33-9b0c-20fd59260114", "Dr. Vincent Teo", "Family Medicine", "https://randomuser.me/api/portraits/men/64.jpg", currentDateTime)
        ];

        _patientConsultations =
        [
            new(Guid.NewGuid(), _availableDoctors[0].Id, _availableDoctors[0].Name, _availableDoctors[0].Specialty, "Chat", currentDateTime.Date.AddDays(1).AddHours(15), "A routine health check-in.", "Scheduled", DemoPatientId),
            new(Guid.NewGuid(), _availableDoctors[1].Id, _availableDoctors[1].Name, _availableDoctors[1].Specialty, "Video", currentDateTime.Date.AddDays(-2).AddHours(10), "Follow-up consultation.", "Completed", DemoPatientId),
            new(Guid.NewGuid(), _availableDoctors[2].Id, _availableDoctors[2].Name, _availableDoctors[2].Specialty, "Chat", currentDateTime.Date.AddDays(-5).AddHours(14), "Discuss recent test results.", "Completed", DemoPatientId)
        ];
    }

    public IReadOnlyList<Doctor> GetDoctors()
    {
        lock (_consultationsLock)
        {
            return _availableDoctors.ToArray();
        }
    }

    public DoctorPage GetDoctors(DoctorQuery query)
    {
        lock (_consultationsLock)
        {
            IEnumerable<Doctor> filteredDoctors = _availableDoctors;

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = query.Search.Trim();
                filteredDoctors = filteredDoctors.Where(doctor =>
                    doctor.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    doctor.Specialty.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(query.Specialty))
            {
                filteredDoctors = filteredDoctors.Where(doctor =>
                    string.Equals(doctor.Specialty, query.Specialty.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            var matchedDoctors = filteredDoctors.OrderBy(doctor => doctor.Name).ToArray();
            var pageSize = Math.Clamp(query.PageSize, 1, DoctorQuery.MaxPageSize);
            var totalPages = Math.Max(1, (int)Math.Ceiling(matchedDoctors.Length / (double)pageSize));
            var page = Math.Clamp(query.Page, 1, totalPages);

            var specialties = _availableDoctors
                .Select(doctor => doctor.Specialty)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(specialty => specialty)
                .ToArray();

            return new DoctorPage(
                matchedDoctors.Skip((page - 1) * pageSize).Take(pageSize).ToArray(),
                page,
                pageSize,
                matchedDoctors.Length,
                totalPages,
                specialties);
        }
    }

    public IReadOnlyList<Consultation> GetConsultations()
    {
        lock (_consultationsLock)
        {
            return _patientConsultations
                .Where(consultation => consultation.PatientId == DemoPatientId)
                .OrderByDescending(consultation => consultation.PreferredDateTime)
                .ToArray();
        }
    }

    public Consultation? CreateConsultation(CreateConsultationRequest bookingRequest)
    {
        if (bookingRequest.PreferredDateTime is not { } preferredDateTime)
        {
            return null;
        }

        lock (_consultationsLock)
        {
            var doctorIndex = _availableDoctors.FindIndex(doctor => doctor.Id == bookingRequest.DoctorId);
            if (doctorIndex < 0)
            {
                return null;
            }

            var selectedDoctor = _availableDoctors[doctorIndex];
            var availableSlots = selectedDoctor.AvailableSlots ?? [];
            var matchedSlot = availableSlots.FirstOrDefault(slot => slot == preferredDateTime);
            if (matchedSlot == default)
            {
                return null;
            }

            var newConsultation = new Consultation(
                Guid.NewGuid(),
                selectedDoctor.Id,
                selectedDoctor.Name,
                selectedDoctor.Specialty,
                bookingRequest.ConsultationType,
                matchedSlot,
                bookingRequest.ReasonForVisit.Trim(),
                "Scheduled",
                DemoPatientId);

            _patientConsultations.Add(newConsultation);
            _availableDoctors[doctorIndex] = selectedDoctor with
            {
                AvailableSlots = availableSlots.Where(slot => slot != matchedSlot).ToArray()
            };

            return newConsultation;
        }
    }

    private static Doctor CreateDoctor(string id, string name, string specialty, string imageUrl, DateTimeOffset now)
    {
        var firstAvailableDay = now.Date.AddDays(1);
        var slots = Enumerable.Range(0, 7)
            .SelectMany(day => new[] { 9, 11, 14, 16, 19 }
                .Select(hour => new DateTimeOffset(firstAvailableDay.AddDays(day).AddHours(hour), now.Offset)))
            .ToArray();

        return new Doctor(Guid.Parse(id), name, specialty, "Appointments available", imageUrl, true, slots);
    }
}
