namespace wcho2BAIS3150CodeSample.Models
{
    public class Program
    {

        public string? ProgramCode { set; get; } = string.Empty;
        public string? Description { set; get; } = string.Empty;

        private readonly List<Student> _EnrolledStudents;
        public List<Student> EnrolledStudents
        {
            get
            {
                return _EnrolledStudents;
            }
        }

        public Program()
        {
            _EnrolledStudents = new();
        }
    }
}
