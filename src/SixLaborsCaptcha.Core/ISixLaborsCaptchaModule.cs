namespace SixLaborsCaptcha.Core
{
	public interface ISixLaborsCaptchaModule
	{
		byte[] Generate(string stringText);
	}
}
