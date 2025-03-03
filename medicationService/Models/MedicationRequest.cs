using Amazon.Internal;
using System;

namespace medicationService.Models
{
    public class MedicationRequest
    {
        public string PatientReference { get; set; }
        public string ClinicianReference { get; set; }
        public string MedicationReference { get; set; }
        public string ReasonText { get; set; }
        public DateTime PrescribedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Frequency { get; set; }
        public MedicationStatus Status { get; set; }
    }
}
