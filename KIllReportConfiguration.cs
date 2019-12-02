using Rocket.API;

namespace KillReportUI
{
    public class KIllReportConfiguration : IRocketPluginConfiguration
    {
        public ushort StyleNumber;
        public void LoadDefaults()
        {
            StyleNumber = 1;
        }
    }
}