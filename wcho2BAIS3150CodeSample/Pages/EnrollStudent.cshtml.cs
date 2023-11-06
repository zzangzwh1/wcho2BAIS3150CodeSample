using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class EnrollStudentModel : PageModel
    {
        public List<string> programCodes = new List<string>();

        [BindProperty]
        [Required]
        [RegularExpression(@"^[0-9]+", ErrorMessage = "Student ID :Only Number!")]
        public string StudentID { set; get; } = string.Empty;
        [BindProperty]
        [Required]
        public string FirstName { set; get; } = string.Empty;
        [BindProperty]
        [Required]
        public string LastName { set; get; } = string.Empty;
        [BindProperty]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email Format required ex) wcho2@nait.ca")]
        public string Email { set; get; } = string.Empty;
        public string Message { set; get; } = string.Empty;
        public void OnGet()
        {
            Programs programs = new Programs();
            string query = @"select ProgramCode from Program ";
            programCodes = programs.GetProgramCode(query);

            Message = "Please Fill data into the input field";
        }
        public void OnPost() 
        {
           string emailPattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$";
            Regex emailRegex = new Regex(emailPattern);
            if (FirstName == null || FirstName.Length == 0)
                ModelState.AddModelError("FirstName", "firstName is required");

            if (LastName == null || LastName.Length == 0)
                ModelState.AddModelError("LastName", "lastName is required");

            if (Email == null || Email.Length == 0 || !emailRegex.IsMatch(Email))
            {
                ModelState.AddModelError("Email", "Eamil is reuqired or invalid Format : ex)wcho2@nait.ca");

            }

            if (ModelState.IsValid)
            {
                Message = " ***  Successs ***";
 
            }
            else
            {
                Message = "** Faill!! ** ";
            }



        }
    }
}
