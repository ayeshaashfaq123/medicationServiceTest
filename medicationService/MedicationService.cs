using medicationService.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace medicationService
{
    public class MedicationService : IMedicationService
    {
        public async Task<HttpResponseMessage> GetMedicationData(MedicationStatus medicationStatus, DateTime prescribedDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PostPaitentData(MedicationRequest medication)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            //doing a validation check to see if required fields not are null
            if(medication.PrescribedDate != null && medication.StartDate != null)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            }
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PatchData(string endDate, string frequency, MedicationStatus status)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            return httpResponseMessage;
        }
    }
}
