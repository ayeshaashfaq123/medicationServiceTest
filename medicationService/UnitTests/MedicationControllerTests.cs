using medicationService.Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace medicationService.UnitTests
{
    
    public class MedicationControllerTests
    {
        public readonly Mock<IMedicationService> medicationService;

        [Fact]
        public async Task GivenController_WhenPostDataIsCalled_Than200IsResponded()
        {
            //Arrange
            MedicationRequest medicationRequest = new MedicationRequest()
            {
                PatientReference = "Ayesha",
                ClinicianReference = "test",
                MedicationReference = "tablets",
                ReasonText = "general medication",
                PrescribedDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Frequency = "2",
                Status = MedicationStatus.active
            };
            medicationService.Setup<>

    
        }
    }
}
