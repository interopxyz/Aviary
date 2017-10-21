using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SoundInTheory.DynamicImage;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Macaw.App_Start.DynamicImage), "PreStart")]

namespace Macaw.App_Start
{
	public static class DynamicImage
	{
		public static void PreStart()
		{
			DynamicModuleUtility.RegisterModule(typeof(DynamicImageModule));
		}
	}
}