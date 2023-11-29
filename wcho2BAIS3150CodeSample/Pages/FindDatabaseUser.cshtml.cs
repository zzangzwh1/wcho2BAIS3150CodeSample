using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wcho2BAIS3150CodeSample.Models;


namespace wcho2BAIS3150CodeSample.Pages
{
    public class FindDatabaseUserModel : PageModel
    {
        public required DatabaseUser CurrentUser { get; set; }
        public void OnGet()
        {
            BCS requestDirector = new BCS();
            CurrentUser = requestDirector.FindDatabaseUser();

        }
    }
}
