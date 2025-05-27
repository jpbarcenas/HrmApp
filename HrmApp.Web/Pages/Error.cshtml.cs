using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace HrmApp.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }
        public string? ErrorMessage { get; set; }
        public int? StatusCode { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public bool ShowDevelopmentDetails => HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment();
        public string? ExceptionDetails { get; set; }
        public string? StackTrace { get; set; }

        public void OnGet(string? message = null, int? statusCode = null, [FromQuery] string? details = null)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorMessage = message;
            StatusCode = statusCode;

            if (ShowDevelopmentDetails)
            {
                ExceptionDetails = details;
                StackTrace = HttpContext.Items["StackTrace"]?.ToString();
            }
        }
    }

}
