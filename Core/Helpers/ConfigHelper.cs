using System.Configuration;

namespace Core.Helpers
{
	internal static class ConfigHelper
	{
		private const string OneScreenLocationKey = "OneScreenSolutionLocation";
		public static string OneScreenLocation { get { return ConfigurationManager.AppSettings[OneScreenLocationKey]; } }
	}
}
