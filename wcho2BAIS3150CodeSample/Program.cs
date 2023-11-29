namespace wcho2BAIS3150CodeSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            var app = builder.Build();


            //configure the HTTP Pipelines
            if(!app.Environment.IsDevelopment()) // check for the production envioronemnt
            {
                app.UseDeveloperExceptionPage(); // not for the production.
                //app.UseExceptionHandler("/Error");  // Customized Error Pageand for  final release

            }

            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();



           //app.MapFallbackToPage("/Index");
            //app.MapGet("/", () => Pages.IndexModel);

            app.Run();
        }
    }
}