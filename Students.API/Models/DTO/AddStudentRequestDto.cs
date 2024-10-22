namespace Students.API.Models.DTO
{
    public class AddStudentRequestDto
    {
        public string FullName { get; set; }
        public int GannonId { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public Guid MajorId { get; set; }
        public Guid HousingId { get; set; }
    }
}
