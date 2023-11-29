using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wcho2BAIS3150CodeSample.Models;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class RemoveStudentModel : PageModel
    {

        public string Message { get; set; } = string.Empty;
        public List<string> StudentId = new List<string>();
        public Student students = null;

        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        public string SelectValue { get; set; } = string.Empty;
        public void OnGet()
        {
            Message = "Please Select Student ID";
            GetStudentID();
        }

        public void OnPostStudent()
        {
            BCS requestDirector = new BCS();
            StudentID = SelectValue;
            students = requestDirector.FindStudent(SelectValue);
         
           // Console.WriteLine(SelectValue);
            GetStudentID();
        }
        public void OnPostDelete()
        {
            GetStudentID();
         
            BCS requestDirector = new BCS();
            bool confirmation = requestDirector.RemoveStudent(StudentID);

            if (ModelState.IsValid || confirmation)
            {

                GetStudentID();
                Message = "Student Is Deleted";
            }
            else
            {
                GetStudentID();
                Message = "Please Try Again";
            }
        }
        public void GetStudentID()
        {
            Students studentID = new Students();
           // string query = @"select StudentID From Student ";
            StudentId = studentID.GetSutdentID();
        }
    }
}
