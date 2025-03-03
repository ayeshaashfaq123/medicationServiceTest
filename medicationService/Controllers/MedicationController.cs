using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using log4net.Core;
using medicationService.Models;
using Newtonsoft.Json;
using Amazon.Runtime.Internal;
using System.Net.Http.Formatting;
using Amazon.Util;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace medicationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase
    {
        
        private readonly ILogger<MedicationController> _logger;
        private readonly IMedicationService _medicationService;

        [HttpPost("postMedicationData", Name = "postMedicationData")]
        public async Task<IActionResult> PostMedication([FromBody] MedicationRequest medicationRequest)
        {
            try
            {
                
                var responseMessage = new HttpResponseMessage();
                _logger.LogInformation($"Post Patient Data {JsonConvert.SerializeObject(medicationRequest)} - TODO TASK");
                responseMessage = await _medicationService.PostPaitentData(medicationRequest);
                _logger.LogInformation($"Post Patient Data  {JsonConvert.SerializeObject(medicationRequest)} - DONE");

                return responseMessage.StatusCode switch
                {
                    HttpStatusCode.OK => await SuccessStatus(responseMessage),
                    _ => ServerErrorStatus(responseMessage, "Unable to process event")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new EventId(999999, "Server error"), ex, "Error from Microservice");
                return StatusCode(500);
            }

        }



        // An example for adding roles authentication to allow only authorised user to retrieve data.
        //[Authorize(Roles = "GetData")]

        //getMedicationData is the name that will appear on swagger once the service is ran
        [HttpGet("getMedicationData", Name = "getMedicationData")]
        [Consumes("application/json")]

        //This is an example of how we can add status codes that the endpoint with respond with. 
        //[ProducesResponseType(typeof(SuccessResponse>), (int)HttpStatusCode.OK)]

        // FromHeader takes the authorization which is passed in ModHeader to execute the endpoint.

        //[FromHeader(Name = HeaderKeys.Authorization)] 
        public async Task<IActionResult> GetHeritageData( [FromQuery] MedicationStatus medicationStatus, DateTime prescribedDate)
        {
            try
            {
                //_logger (ILoggerWrapper) - allows us to monitor the service by viewing this LogInformation logs in kibana
                //this is good because we can view how the service is responding when it is ran on various environments and 
                //view any errors that it is throwing
                _logger.LogInformation("GetMedication Process Start");
                HttpResponseMessage responseMessage = new HttpResponseMessage();

                responseMessage = await _medicationService.GetMedicationData( medicationStatus, prescribedDate);

                return responseMessage.StatusCode switch
                {
                    HttpStatusCode.OK => await SuccessStatus(responseMessage),
                    HttpStatusCode.BadRequest => await BadRequestError(responseMessage, $"unable to retrieve data "),
                    HttpStatusCode.Unauthorized => await UnauthorizedError($"Unauthorized"),
                    HttpStatusCode.NotFound => await NotFoundError(responseMessage, $"Not Found"),
                    _ => ServerErrorStatus(responseMessage, "Unable to process your request")
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new EventId(999999, "Server error"), ex, "Service with error");
                return StatusCode(500);
            }
        }

        [HttpPatch("updateData")]
        public async Task<IActionResult> PatchData( [FromQuery(Name = "endDate")][Required] string endDate, [FromQuery(Name = "frequency")] string frequency,
                                                                 [FromQuery] MedicationStatus status)
        {
            HttpResponseMessage responseMessages = new HttpResponseMessage();
            try
            {
                _logger.LogInformation($"Calling PatchData for endDate: {endDate}  , frequency: {frequency} and status: {status}" );

                if( endDate == null && frequency == null && status == null)
                {
                    return await BadRequestError(responseMessages, "Values cannot be Null");
                }

                responseMessages = await _medicationService.PatchData(endDate, frequency, status).ConfigureAwait(false);

                return responseMessages.StatusCode switch
                {
                    HttpStatusCode.OK => await SuccessStatus(responseMessages),
                    HttpStatusCode.BadRequest => await BadRequestError(responseMessages, $"unable to retrieve data "),
                    HttpStatusCode.Unauthorized => await UnauthorizedError($"Unauthorized"),
                    HttpStatusCode.NotFound => await NotFoundError(responseMessages, $"Not Found"),
                    _ => ServerErrorStatus(responseMessages, "Unable to process your request")
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, new EventId(999999, "Server error"), ex, "Error from USM/SCM Microservice");
                return ServerErrorStatus(new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError }, "Error from USM/SCM Microservice");
            }
        }

        private IActionResult ServerErrorStatus(HttpResponseMessage responseMessage, string message)
        {
            var errorMessage = $"Error from Microservice {message}. Service responded status code" + responseMessage.StatusCode;
            _logger.LogError(errorMessage, new EventId(999999, "Server error"), null, "Error from USM/SCM Microservice");

            ErrorResponse error = new ErrorResponse
            {
                Message = message,
                Code = "Exception"
            };
            return StatusCode((int)HttpStatusCode.InternalServerError, error);
        }

        private async Task<IActionResult> NotFoundError(HttpResponseMessage responseMessage, string errorMessage)
        {
            ErrorResponse errorResponse = new ErrorResponse()
            {
                Message = errorMessage,
                Code = "Exception"
            };

            var formatter = new JsonMediaTypeFormatter();
            responseMessage.Content = new ObjectContent<ErrorResponse>(errorResponse, formatter, "application/json");
            return StatusCode((int)HttpStatusCode.NotFound, errorResponse);
        }

        private async Task<IActionResult> UnauthorizedError(string errorMessage1)
        {
            string errorMessage = $"{errorMessage1}";
            var validationStr = JsonConvert.SerializeObject(errorMessage, Newtonsoft.Json.Formatting.Indented);
            _logger.LogWarning(errorMessage, validationStr);
            return Unauthorized(errorMessage.ToArray());
        }

        private async Task<IActionResult> SuccessStatus(HttpResponseMessage responseMessage)
        {
            //ReadAsStringAsync - this to serialise the HTTP code as a string
            // when the status is = 200 
            //than this block os code is executed and logging information to kibana 
            // and responding with 200 status code in swagger 
            var content = await responseMessage.Content?.ReadAsStringAsync();
            _logger.LogInformation("Successfully completed", content);
            return Ok(JsonConvert.DeserializeObject(content));
        }


        private async Task<IActionResult> BadRequestError(HttpResponseMessage responseMessage, string message)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();
            object errorResponse = JsonConvert.DeserializeObject(content);
            string errorMessage = $"Bad Request from the service {message}";
            var validationStr = JsonConvert.SerializeObject(errorResponse, Newtonsoft.Json.Formatting.Indented);
            _logger.LogWarning(errorMessage, validationStr);
            return BadRequest(errorResponse);
        }
    }
}
