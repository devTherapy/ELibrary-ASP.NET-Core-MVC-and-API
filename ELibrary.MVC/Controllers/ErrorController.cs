using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.MVC.Controllers
{
    public class ErrorController : Controller
    {
       
        [AllowAnonymous]
        [Route("/Error/{statusCode}")]
        public IActionResult ErrorHandler(int statusCode)
        {

            return View("NotFoundPage");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ServerError()
        {
            var ExceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ErrMsg = ExceptionFeature.Error.Message;
            // and others to be logged.
            return View("ServerErrorPage");
        }
    }
}
