using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wcho2BAIS3150CodeSample.Models;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class FindProgramModel : PageModel
    {
        public Models.Program ActivePrgoram = null;
        public List<string> ProgramCode = new List<string>();
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        public string SelectValue { get; set; } = string.Empty;

        public void OnGet()
        {
  /*         BCS requestDirector = new BCS();
            ActivePrgoram = requestDirector.FindProgram("BAIST");
            string s = "";*/
            GetProgramCode();
            Message = "Please Select Exists Program Code";
        }
        public void OnPost()
        {
            

         
            if (string.IsNullOrEmpty(SelectValue))
            {
                ModelState.AddModelError("SelectValue", "SelectValue Must be Selected!");

            }
          

            if (ModelState.IsValid )
            {
                string s = "";
                BCS requestDirector = new BCS();
                ActivePrgoram = requestDirector.FindProgram(SelectValue);

                GetProgramCode();
                

            }
            else
            {
                GetProgramCode();
                Message = "You Did Not select the ProgramCode, Please Select ProgramCode";
            }
        }
        public void GetProgramCode()
        {
            Programs programs = new Programs();
            string query = @"select ProgramCode from Program ";
            ProgramCode = programs.GetProgramCode(query);
        }
    }
}
