using HealthLinkAPI.Models;

namespace HealthLinkAPI.Services;

public interface IHealthLinkStore
{
    IReadOnlyList<Doctor> GetDoctors();

    DoctorPage GetDoctors(DoctorQuery query);

    IReadOnlyList<Consultation> GetConsultations();

    Consultation? CreateConsultation(CreateConsultationRequest bookingRequest);
}
