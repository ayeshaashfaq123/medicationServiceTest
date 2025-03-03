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

            //We are going to be callling GetAsync methods in our Get endpoints to fetch the requested data. 
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

            //Once the validation check is completed and required fields are present 
            // In a developement environment, where we have database such as  DynamoDB operations are backed up 
            // we will be calling CreateAsync methods in a repository, which call the dynamoDB table 
            //mentioned in appsettings of the service to create that particular data in . 
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
