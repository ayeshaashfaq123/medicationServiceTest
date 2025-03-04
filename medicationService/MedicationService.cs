using medicationService.Controllers;
using medicationService.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace medicationService
{
    public class MedicationService : IMedicationService
    {
        private readonly ILogger<MedicationController> _logger;

        public MedicationService(ILogger<MedicationService> logger)
        {
            //_logger = new 
        }
        public async Task<HttpResponseMessage> GetMedicationData(MedicationStatus medicationStatus, DateTime prescribedDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            if(medicationStatus != null && prescribedDate != null )
            {
                //We are going to be callling GetAsync methods in our Get endpoints to fetch the requested data.
                //perform dynamo operations
            }



            httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PostPaitentData(MedicationRequest medication)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            //doing a validation check to see if required fields not are null
            if(medication.PrescribedDate == null && medication.StartDate == null)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _logger.LogInformation("Required fields cannot be null");
            }
            else
            {
                if(medication.PrescribedDate != null && medication.StartDate != null)
                {
                    //calling the database here 
                    //methods like CreateAsync can be called using the dynamo operations 
                    //to execute and connecting to the table. 

                }
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
