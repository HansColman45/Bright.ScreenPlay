namespace Bright.ScreenPlay.Settings
{
    public class ScreenPlaySettingsBuilder
    {
        private bool _takeScreenShot;
        private string _baseUrl;

        private ScreenPlaySettingsBuilder()
        {
        }

        public static ScreenPlaySettingsBuilder Empty()
        {
            return new();
        }
        public ScreenPlaySettingsBuilder WithTakeScreenShot(bool screenshot) 
        {
            _takeScreenShot = screenshot;
            return this;
        }
        public ScreenPlaySettingsBuilder WithBaseUrl(string baseUrl) 
        {
            _baseUrl = baseUrl;
            return this;
        }
        public ScreenPlaySettings Build()
        {
            return new ScreenPlaySettings
            {
                TakeScreenShot = _takeScreenShot,
                BaseUrl = _baseUrl
            };
        }
    }
}
