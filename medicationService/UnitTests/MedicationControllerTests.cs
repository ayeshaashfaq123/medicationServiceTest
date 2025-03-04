using Amazon.Glacier;
using Amazon.SimpleEmail.Model;
using AutoFixture;
using medicationService.Models;
using Moq;
using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using medicationService.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace medicationService.UnitTests
{
    
    public class MedicationControllerTests
    {
        //mocking the service that contains the methods for our controller to be tested through
        //autoFixture is a good technique as it allowes us for using the Arrange step of the unit tests
        //and is easier to write tests with
        //Adding the controller as well to  add the POST/GET Endpoints
        public readonly Mock<IMedicationService> _medicationService;
        private readonly Fixture _fixture;
        private MedicationController _controller;


        public MedicationControllerTests()
        {
            _medicationService = new Mock<IMedicationService>();
            _fixture = new Fixture();
            _controller = new MedicationController(_medicationService.Object);
        }

        [Fact]
        public async Task GivenController_WhenPostDataIsCalled_Than200IsResponded()
        {
            //Arrange
            var request = _fixture.Build<MedicationRequest>().Create();
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{}")
            };

            _medicationService.Setup(service => service.PostPaitentData(It.IsAny<MedicationRequest>())).
                ReturnsAsync(responseMessage);

            //Act
            _controller = new MedicationController(_medicationService.Object);
            var result = await _controller.PostMedication(request) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GivenController_WhenGetMedicationDataIsCalled_ThanBadRequetIsResponded()
        {
            //Arrange
            var request = _fixture.Build<MedicationStatus>().Create();
            var prescribedDate = DateTime.UtcNow;
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("{}")
            };

            _medicationService.Setup(service => service.GetMedicationData(It.IsAny<MedicationStatus>(), It.IsAny<DateTime>())).
                ReturnsAsync(responseMessage);

            //Act
            _controller = new MedicationController(_medicationService.Object);
            var result = await _controller.GetMedicationData(request, prescribedDate) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
