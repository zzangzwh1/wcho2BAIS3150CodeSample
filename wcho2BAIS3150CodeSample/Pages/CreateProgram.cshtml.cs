using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wcho2BAIS3150CodeSample.Models;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class CreateProgramModel : PageModel
    {
        public string Message { set; get; } = string.Empty;

        [BindProperty]
        public string programCode { set; get; } = string.Empty;
        [BindProperty]
        public string description { set; get; } = string.Empty;
        public void OnGet()
        {
            Message = "Please fill in data in the input field";
        }
        public void OnPost()
        {
          
            if (string.IsNullOrEmpty(programCode))
            {
                ModelState.AddModelError("programCode", "programCode is required");
            }
            if(string.IsNullOrEmpty(description))
            {
                ModelState.AddModelError("description", "description is required");
            }

            BCS requestDirector = new BCS();
            Models.Program programs = new()
            {
                ProgramCode = programCode,
                Description = description

            };
            bool isValid = requestDirector.CreateProgram(programs.ProgramCode, programs.Description);

            if (ModelState.IsValid && isValid)
            {
               
               /* if (!isValid)
                {
                    Message = "Not Valid";
                }*/

                Message = "Successsfully Program is Created!";
              
                programCode = string.Empty;
                description = string.Empty;
              
            }
            else
            {
                Message = "Program Code is exists Please Try Again!";
            }
        }
    }
}
