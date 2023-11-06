using System;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Models
{
    public class BCS
    {
        public bool EnrollStudent(Student acceptedStudent, string programCode)
        {
        
            Students studentManager = new Students();
            bool success = studentManager.AddStudent(acceptedStudent, programCode);

            return success;

        }
        public bool CreateProgram(string programCode, string description)
        {
            Programs programManager = new Programs();

            bool success = programManager.AddProgram(programCode, description);

            return success;
        }
        public Student FindStudent(string studentId)
        {
            Students studentManager = new Students();
            Student enrolledStudent = studentManager.GetStudent(studentId);


            return enrolledStudent;
        }
        public bool ModifyStudent(Student student)
        {
            bool success = false;
            Students studentManager = new Students();
           // bool success = studentManager.UpdateStudent(student);
            return success;


        }
        public bool RemoveStudent(string studentId)
        {
            Students studentManager = new Students();
            bool success = studentManager.DeleteStudent(studentId);
            return success;
        }
        public Program FindProgram(string findProgramCode)
        {
            Programs programManager = new Programs();
            Program activeProgram = programManager.GetProgram(findProgramCode);

            return activeProgram;
        }
    }
}
