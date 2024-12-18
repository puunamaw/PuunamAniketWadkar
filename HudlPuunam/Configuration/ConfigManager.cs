using System.Text.Json;

namespace HudlPuunam.Configuration
{
    public class ConfigManager
    {

        public static Dictionary<string, object> RunSettings()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // This ensure the path remains agnostic to the operating system.
            string filePath = Path.Combine(currentDirectory, "AppSettings", "AppSettings.json");

            // Check if the file exists.
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            // Get the file contents. 
            string jsonContent = File.ReadAllText(filePath);

            // Deserialise into POJO.
            AppSettings? appSettings = JsonSerializer.Deserialize<AppSettings>(json: jsonContent);
            TestEnvironment? testEnvironment = appSettings?.TestEnvironment.FirstOrDefault(x => x.Environment == appSettings.RunOnEnvironment);

            // Load the dictionary with run settings.
            var settingsDictionary = new Dictionary<string, object>
            {
                { ConfigManagerConstraints.RunOnEnvironment, appSettings.RunOnEnvironment },
                { ConfigManagerConstraints.RunOnBrowser, appSettings.RunOnBrowser },
                { ConfigManagerConstraints.ImplicitWait, appSettings.ImplicitWait },
                { ConfigManagerConstraints.TestEnvironment, appSettings.TestEnvironment },
                { ConfigManagerConstraints.Email, testEnvironment.Email },
                { ConfigManagerConstraints.Password, testEnvironment.Password},
                { ConfigManagerConstraints.Url, testEnvironment.Url },
                { ConfigManagerConstraints.PageLoadTimeout, appSettings.PageLoadTimeout},
                { ConfigManagerConstraints.HeadlessMode, appSettings.HeadlessMode }
            };

            return settingsDictionary;
        }
    }
}
