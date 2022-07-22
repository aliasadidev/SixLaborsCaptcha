using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SixLaborsCaptcha.Core;

namespace AspNetCoreWebAppSample.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("[action]")]
		public FileResult GetCaptchaImage([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha)
		{
			string key = Extensions.GetUniqueKey(6);
			var imgText = sixLaborsCaptcha.Generate(key);
			return File(imgText, "Image/Png");
		}
	}
}
