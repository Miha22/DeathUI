using Rocket.API;

namespace KillReportUI
{
    public class KIllReportConfiguration : IRocketPluginConfiguration
    {
        public ushort StyleNumber;
        public string LicenseKey;
        public void LoadDefaults()
        {
            StyleNumber = 1;
            LicenseKey = System.Guid.Empty.ToString();
        }
    }
}