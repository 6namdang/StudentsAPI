namespace Students.API.Models.DTO
{
    public class UpdateStudentRequestDto
    {
        public string FullName { get; set; }
        public int GannonId { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public MajorDto Major { get; set; }
        public HousingDto Housing { get; set; }
    }
}
