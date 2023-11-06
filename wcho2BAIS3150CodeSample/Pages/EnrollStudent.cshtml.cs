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
