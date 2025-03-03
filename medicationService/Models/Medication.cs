namespace medicationService.Models
{
    public class Medication
    {
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeSystem { get; set; }
        public string StrengthValue { get; set; }
        public string StrengthUnit { get; set; }
        public Form Form { get; set; }
    }
}
