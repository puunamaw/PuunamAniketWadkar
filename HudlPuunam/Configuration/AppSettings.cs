namespace HudlPuunam.Configuration
{
    public class AppSettings
    {
        public string RunOnEnvironment { get; set; }

        public string RunOnBrowser { get; set; }

        public int ImplicitWait { get; set; }

        public List<TestEnvironment> TestEnvironment { get; set; }

        public int PageLoadTimeout { get; set; }

        public bool HeadlessMode { get; set; }
    }
}
