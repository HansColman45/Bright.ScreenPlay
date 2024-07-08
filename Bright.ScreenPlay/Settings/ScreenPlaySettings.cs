namespace Bright.ScreenPlay.Settings
{
    public class ScreenPlaySettings
    {
        public bool TakeScreenShot { get; set; }
        public string BaseUrl { get; set; }
    }
    public class WebSettings : ScreenPlaySettings
    {
        public int LongTimeOutInMilSec { get; set; }
        public int LongTimeOutInSec { get; set; }
        public int ShortTimeOutInMilSec { get; set; }
        public int ShortTimeOutInSec { get; set; }
        public int PollingIntervallInMilSec { get; set; }

        public WebSettings()
        {
            LongTimeOutInMilSec = 1000;
            LongTimeOutInSec = 20;
            ShortTimeOutInMilSec = 500;
            ShortTimeOutInSec = 1;
            PollingIntervallInMilSec = 150;
        }
    }
}
