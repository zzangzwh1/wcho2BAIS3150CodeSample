using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wcho2BAIS3150CodeSample.Models;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class FindStudentModel : PageModel
    {
        public List<string> StudentId = new List<string>();
        public string Message { get; set; } = string.Empty;


        [BindProperty]
        public string selectValue { get; set; } = string.Empty;

        public Student students = null;
        // public List<string> studentInfoFields = new List<string>();

        public void OnGet()
        {
            GetStudentID();
            Message = "Please select StudentId";
        }
        public void OnPost()
        {

            if (string.IsNullOrEmpty(selectValue))
            {
                ModelState.AddModelError("selectValue", "selectValue Must be Selected!");

            }


            if (ModelState.IsValid)
            {

                GetStudentID();
                BCS requestDirector = new BCS();


                students = requestDirector.FindStudent(selectValue);


            }
            else
            {
                GetStudentID();
                Message = "You Did Not select the StudentId, Please Select Student ID";
            }
        }

        public void GetStudentID()
        {
            Students studentID = new Students();
            string query = @"select StudentID From Student ";
            StudentId = studentID.GetSutdentID(query);
        }
    }
}
