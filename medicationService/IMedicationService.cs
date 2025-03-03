using medicationService.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace medicationService
{
    public interface IMedicationService
    {
        Task<HttpResponseMessage> PostPaitentData(MedicationRequest medication);
        Task<HttpResponseMessage> GetMedicationData(MedicationStatus medicationStatus, DateTime prescribedDate);
        Task<HttpResponseMessage> PatchData(string endDate, string frequency, MedicationStatus status);
    }
}
