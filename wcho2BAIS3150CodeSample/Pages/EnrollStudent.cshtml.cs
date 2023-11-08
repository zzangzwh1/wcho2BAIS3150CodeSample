using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using wcho2BAIS3150CodeSample.Models;
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
        public string _FirstName { set; get; } = string.Empty;
        [BindProperty]
        [Required]
        public string LastName { set; get; } = string.Empty;

        [BindProperty]
        [RegularExpression(@"^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})?$", ErrorMessage = "Invalid email format ex)wcho2@nait.ca")]
        public string? Email { set; get; } = string.Empty;

        [BindProperty]
        [Required]
        public string programCode { set; get; } = string.Empty;
        public string Message { set; get; } = string.Empty;
        public void OnGet()
        {
          
            GetProgramCode();
            Message = "Please Fill data into the input field";
        }
        public void OnPost() 
        {
            BCS requestDirector = new BCS();
            Student accepetedStudent = new()
            {
                StudentId = StudentID,
                FirstName = _FirstName,
                lastName = LastName,
                Email = Email

            };
         

            /* Models.Program p = new Models.Program();
             p.ProgramCode = ;*/
            bool confirmation = requestDirector.EnrollStudent(accepetedStudent, programCode);

            if (ModelState.IsValid && confirmation)
            {
               

                Message = " ***  Successs ***";
                GetProgramCode();

                //does not clear
                // Clear the input fields after success
                StudentID = string.Empty;
                _FirstName = string.Empty;
                LastName = string.Empty;
                Email = string.Empty;



            }
            else
            {
                
                GetProgramCode();
                Message = "** Faill!! ** ";
            }

         

        }
        public void GetProgramCode()
        {
            Programs programs = new Programs();
            string query = @"select ProgramCode from Program ";
            programCodes = programs.GetProgramCode(query);
        }
   /*     public void isStudentIdExist()
        {
            Students studentId = new Students();
        }*/
    }
}
