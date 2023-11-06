namespace wcho2BAIS3150CodeSample.Models
{
    public class Student
    {
        // _CamelCase for member field
        private string? _studentId = string.Empty;
        private string? _firstName = "";



        public string? StudentId  //pascal case
        {
            get
            {
                return _studentId;
            }
            set
            {
                _studentId = value;
            }

        }
        //Expression-Bodied Property Assessor
        public string? FirstName
        {
            get => _firstName; // implementation can be made up of only a single statement
            set => _firstName = value;
        }
        // auto-impletemented property . no logic in get /set
        public string? lastName { get; set; } = "";
        public string? Email { get; set; }

        //Constructor
        public Student()
        {

        }
    }
}
