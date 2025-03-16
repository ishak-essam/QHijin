namespace QHijin.Models.Dto
{
    public class trainerDto
    {
        public int trnId { get; set; }
        public int? itemId { get; set; }
        public int empId { get; set; }
        public string rpayment_link { get; set; }
        public double rentmoney { get; set; }
        public IFormFile? Image { get; set; }
        public string FullName { get; set; }
        public bool status { get; set; } = true;
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? trnImgLocalPath { get; set; }
        public IFormFile? Cv { get; set; }
    }
}
