using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wcho2BAIS3150CodeSample.Models;
using wcho2BAIS3150CodeSample.TechService;

namespace wcho2BAIS3150CodeSample.Pages
{
    public class FindProgramModel : PageModel
    {
        public List<string> ProgramCode = new List<string>();
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        public string SelectValue { get; set; } = string.Empty;

        //public Models.Program ActivePrgoram = null;
        public void OnGet()
        {
            BCS requestDirector = new BCS();
            Models.Program ActivePrgoram = requestDirector.FindProgram("BAIST");
            string s = "";
            GetProgramCode();
            Message = "Please Select Exists Program Code";
        }
        public void OnPost()
        {
         
            if (string.IsNullOrEmpty(SelectValue))
            {
                ModelState.AddModelError("SelectValue", "selectValue Must be Selected!");

            }
            BCS requestDirector = new BCS();
            Models.Program ActivePrgoram = requestDirector.FindProgram(SelectValue);
            string s = "";
            foreach (var a in ActivePrgoram.EnrolledStudents)
            {
                Console.WriteLine(a);
            }

            if (ModelState.IsValid )
            {
             
           
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
