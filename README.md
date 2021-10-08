# SixLaborsCaptcha
Generating image captcha with .net core/.net 5+ in both `Linux` and `Windows` environments 

## Features

- Simple & Cross-Platform
- Compatible with Linux and Windows
- Compatible with Docker images based on Linux :)


# What's New

## Version 0.1.3 - Oct 8, 2021
#### Add
* Add NoiseRateColor/NoiseRate options
* Add MinLineThickness/MaxLineThickness options
#### Change
* Improve the rendering speed
* Improve the captcha image security


## Packages

|Package|Description|
|---------|-----------|
|SixLaborsCaptcha.Core|Using for ConsoleApp, WebAPI, WinForms, and etc..|
|SixLaborsCaptcha.Mvc.Core|Using for ASP.NET MVC Core and ASP.NET Web API Core|

## SixLaborsCaptchaOptions

|Property|Description|
|---------|-----------|
|FontFamilies| Characters fonts, default is "Arial", "Verdana", "Times New Roman" <br/> `Notice: This default fonts working only on Windows, if you want to run it on Linux you have to use the Linux fonts`|
|TextColor|  Characters colors, default is { Color.Blue, Color.Black, Color.Black, Color.Brown, Color.Gray, Color.Green } |
|DrawLinesColor| Line colors, default is { Color.Blue, Color.Black, Color.Black, Color.Brown, Color.Gray, Color.Green }|
|Width| Width of image box, default is 180 |
|Height| Height of image box, default is 50 |
|FontSize| Font size, default is 29 |
|FontStyle| Font Style: Regular,Bold,Italic,BoldItalic |
|EncoderType| Result file formant: Jpeg,Png|
|DrawLines| Draw the random lines, default is 5|
|MaxRotationDegrees| Rotation degrees, default is 5|
|MinLineThickness| Min Line Thickness, default is 0.7f|
|MaxLineThickness| Max Line Thickness, default is 2.0f|
|NoiseRate| Noise Rate, default is 800|
|NoiseRateColor| Noise colors, default is { Color.Gray }|


## Install via NuGet

### To install SixLaborsCaptcha.Core, run the following command in the Package Manager Console: ###

```
PM> Install-Package SixLaborsCaptcha.Core
```

## Usage:
```csharp
using SixLaborsCaptcha.Core;
using SixLabors.ImageSharp;
using System.IO;

namespace ConsoleAppSample
{
	class Program
	{
		static void Main(string[] args)
		{
			var slc = new SixLaborsCaptchaModule(new SixLaborsCaptchaOptions
			{
				DrawLines = 7,
				TextColor = new Color[] { Color.Blue, Color.Black },
			});

			var key = Extentions.GetUniqueKey(6);
			var result = slc.Generate(key);
			File.WriteAllBytes($"six-labors-captcha.png", result);
		}
	}
}

```
![result](/samples/images/six-labors-captcha-3.png?raw=true "six-labors-captcha")


### To install SixLaborsCaptcha.Mvc.Core for ASP.NET Core MVC, run the following command in the Package Manager Console: ###
```
PM> Install-Package SixLaborsCaptcha.Mvc.Core
```
## Usage:
1. In the ConfigureServices method of Startup.cs, register the AddSixLabCaptcha generator

```csharp
using SixLaborsCaptcha.Mvc.Core;
...
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSixLabCaptcha(x =>
			{
				x.DrawLines = 4;
			});
		}
...
```
2. Create an action to generate image
```csharp
using SixLaborsCaptcha.Core;
...

[HttpGet]
[Route("[action]")]
public FileResult GetCaptchaImage([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha)
{
	string key = Extentions.GetUniqueKey(6);
	var imgText = sixLaborsCaptcha.Generate(key);
	return File(imgText, "Image/Png");
}

...
```
3. Get the image from action
```html
<div class="text-center">
	<h1 class="display-4">Welcome to SixLaborsCaptcha</h1>
	<img src='@Url.Action("GetCaptchaImage","Home")?v1' />
	<br />
	<img src='@Url.Action("GetCaptchaImage","Home")?v2' />
	<br />
	<img src='@Url.Action("GetCaptchaImage","Home")?v3' />
	<br />
	<img src='@Url.Action("GetCaptchaImage","Home")?v4' />
</div>
```
![result](/samples/images/six-labors-captcha-1.png?raw=true "six-labors-captcha")
![result](/samples/images/six-labors-captcha-2.png?raw=true "six-labors-captcha")
![result](/samples/images/six-labors-captcha-4.png?raw=true "six-labors-captcha")
![result](/samples/images/six-labors-captcha-5.png?raw=true "six-labors-captcha")


### Run asp.net core mvc app on Linux os: ###
1. Download a font (also, you can use the system fonts and don't needs to do this step)
```
wget -O ~/Downloads/marlboro.zip https://www.1001freefonts.com/d/3761/marlboro.zip
unzip -p ~/Downloads/marlboro.zip Marlboro.ttf > ~/Downloads/Marlboro.ttf
rm ~/Downloads/marlboro.zip
cp ~/Downloads/Marlboro.ttf ~/.fonts/
```
2. Config the serivce
```csharp

services.AddSixLabCaptcha(x => {
			   x.FontFamilies = new string[] { "Marlboro" };
		         });
```
![linux-result](/samples/images/six-labors-captcha-6.png?raw=true "six-labors-captcha-linux")
