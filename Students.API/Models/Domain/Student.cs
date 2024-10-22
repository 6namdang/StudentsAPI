using System.Runtime.InteropServices.Marshalling;

namespace Students.API.Models.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int GannonId { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public Guid MajorId { get; set; }
        public Guid HousingId { get; set; }

        //navigation
        public Major Major { get; set; }
        public Housing Housing { get; set; }
    }
}
