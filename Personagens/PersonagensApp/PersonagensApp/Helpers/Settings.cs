// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PersonagensApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}


        public static string UserEmail
        {
            get => AppSettings.GetValueOrDefault("UserEmail", string.Empty);
            set => AppSettings.AddOrUpdateValue("UserEmail", value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault("AccessToken", string.Empty);
            set => AppSettings.AddOrUpdateValue("AccessToken", value);
        }

    }
}