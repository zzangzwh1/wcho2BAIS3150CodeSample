using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using wcho2BAIS3150CodeSample.Models;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class ModifyStudentModel : PageModel
    {
        public List<string> StudentId = new List<string>();
        public string Message { get; set; } = string.Empty;

        [Required(ErrorMessage = "Select a Student ID")]
        [BindProperty]
        public string SelectValue { get; set; }

        public Student students = null;

        [BindProperty]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[A-Za-z\-']+$", ErrorMessage = "Invalid Name format, e.g) Mike")]
        public string _FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[A-Za-z\-']+$", ErrorMessage = "Invalid Name format, e.g) cho")]
        public string _LastName { get; set; }

        [BindProperty]
        public string _StudentID { get; set; }

        [BindProperty]
       
        [RegularExpression(@"^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})?$", ErrorMessage = "Invalid email format, e.g., wcho2@nait.ca")]
        public string? _Email { get; set; }

        public void OnGet()
        {
            GetStudentID();
            Message = "Please select Student ID";
        }

        public void OnPostStudent()
        {          

            BCS requestDirector = new BCS();
            students = requestDirector.FindStudent(SelectValue);
            GetStudentID();
        }

        public void OnPostUpdate()
        {
            /*  ;    
                    students.FirstName = _FirstName;
                    students.lastName = _LastName;
                    students.Email = _Email;*/

            // student
            string s = "";
            students = new() { 
             StudentId = _StudentID,
             Email =_Email,
             FirstName = _FirstName,
             lastName =_LastName

            };
            BCS requestDirector = new BCS();
            bool confirmation = requestDirector.ModifyStudent(students);
            // int count = 0; 
            if (ModelState.IsValid || confirmation)
            {
                // Update the student's information here

                // Save the changes or perform your desired action
                // requestDirector.UpdateStudent(students);

                // Optionally, clear the form fields
                // _FirstName = _LastName = _Email = string.Empty;
                GetStudentID();
                Message = "Students Info Is Updated";
            }
            else
            {
                GetStudentID();
              /*  GetStudentID();
                BCS requestDirector = new BCS();
                students = requestDirector.FindStudent(SelectValue);*/
                Message = "Invalid";
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
