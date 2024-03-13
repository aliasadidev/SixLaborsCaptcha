using Microsoft.AspNetCore.Mvc;
using SixLaborsCaptcha.Core;

namespace AspNetCoreWebAppSample.Controllers;

public class HomeController : Controller
{
  public IActionResult Index() => View();

  [HttpGet]
  [Route("[action]")]
  public FileResult GetCaptchaImage(
    [FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha
  )
  {
    var key = Extensions.GetUniqueKey(6);
    var imgText = sixLaborsCaptcha.Generate(key);
    return File(imgText, "Image/Png");
  }
}
