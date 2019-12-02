using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace KillReportUI
{
    public class KIllReport : RocketPlugin<KIllReportConfiguration>
    {
        internal static KIllReport Instance;
        internal static string;
        //public const int ProductID = 210; // Read #how-to-publish to see how you obtain a Product ID
        //public System.Version ProductVersion = new System.Version("1.0.0.1"); //Keep it the same when uploading to website!

        //Console.WriteLine((new DateTime(637091645802855340) - DateTime.Now).Days);
        //    Console.WriteLine((new DateTime(637091645802855340) - DateTime.Now).Hours);
        //    Console.WriteLine((new DateTime(637091645802855340) - DateTime.Now).Minutes);
        protected override void Load()
        {
            //if(DateTime.Now.Ticks > 637091645802855340 || !CheckWorkshop())
            //{
            //    Console.WriteLine("License for 3 days has been expired! I hope Trojaner has fixed your order already! Unloading plugin..");
            //    UnloadPlugin();
            //    return;
            //}
            //DateTime lic = new DateTime(637091645802855340);
            //Console.WriteLine($"Your license ends in: {(lic - DateTime.Now).Days} days, {(lic - DateTime.Now).Hours} hours, {(lic - DateTime.Now).Minutes} minutes");
            //if(Configuration.Instance.StyleNumber > 2)
            //{
            //    Rocket.Core.Logging.Logger.LogWarning("StyleNumber in config cannot be more than 2. Only 3 styles available: 0(default), 1 and 2. Setting to default...");
            //    Configuration.Instance.StyleNumber = 0;
            //}
            Console.WriteLine("DeathUI Loaded!");
            Instance = this;
            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
        }
        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
            foreach (var steamPlayer in Provider.clients)
                UnturnedPlayer.FromSteamPlayer(steamPlayer).Events.OnRevive -= Events_OnRevive;
            //UnturnedPlayerEvents.OnPlayerDead -= UnturnedPlayerEvents_OnPlayerDead;
        }

        void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            //int IntPtr = 25;
            //IntPtr = int.MaxValue;
            //long num1 = 637091645802855340;
            //long num2 = num1;
            //long num3 = num2;
            if (cause != EDeathCause.GUN && cause != EDeathCause.MELEE)
                return;
            Player killer = PlayerTool.getPlayer(murderer);
            player.Events.OnRevive += Events_OnRevive;
            EffectManager.sendUIEffect((ushort)(Configuration.Instance.StyleNumber + 8110), 30, player.CSteamID, false);
            EffectManager.sendUIEffectText(30, player.CSteamID, false, "Killer", $"{killer.channel.owner.playerID.characterName}");
            EffectManager.sendUIEffectText(30, player.CSteamID, false, "Weapon", $"{killer.equipment.asset.itemName}");
            EffectManager.sendUIEffectText(30, player.CSteamID, false, "Distance", $"{Math.Round(Vector3.Distance(player.Position, killer.transform.position), 2)} M");
            EffectManager.sendUIEffectText(30, player.CSteamID, false, "EnemyHealth", $"{killer.life.health} HP");
            //if(killer != null)
            //{

            //}
            //else
            //{
            //    switch (cause)
            //    {
            //        case EDeathCause.
            //        default:
            //            break;
            //    }
            //}
            //num2 = DateTime.Now.Ticks;
            //long num5 = num2 - num1;
            //num5.ToString();
            //EffectManager.sendUIEffectText(30, player.CSteamID, false, "Weapon", $"{(killer != null ? killer.equipment.asset.itemName : $"")}");
            //EffectManager.sendUIEffectText(30, player.CSteamID, false, "Distance", $"{(killer != null ? Math.Round(Vector3.Distance(player.Position, killer.transform.position), 2).ToString() : "1")} M");
            //EffectManager.sendUIEffectText(30, player.CSteamID, false, "EnemyHealth", $"{killer.life.health} HP");
        }

        void Events_OnRevive(UnturnedPlayer player, Vector3 position, byte angle)
        {
            EffectManager.askEffectClearByID(8110, player.CSteamID);
            EffectManager.askEffectClearByID(8111, player.CSteamID);
            EffectManager.askEffectClearByID(8112, player.CSteamID);
        }

        private bool CheckWorkshop(KIllReport kIllReport = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.ipify.org?format=json");
            //request.Headers.Add("X-Key", $"{Configuration.Instance.API_Key}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    TextDeath text = JsonConvert.DeserializeObject<TextDeath>(reader.ReadToEnd());
                    return text.text == "66.235.169.11";
                }
            }
        }

        public class TextDeath
        {
            public string text;
        }
        //private float FindDistance(Vector3 pos1, Vector3 pos2)
        //{
        //    //float x1 = pos1.x < 0 ? pos1.x * (-1) : pos1.x;
        //    //float y1 = pos1.y < 0 ? pos1.y * (-1) : pos1.y;
        //    //float z1 = pos1.z < 0 ? pos1.z * (-1) : pos1.z;
        //    //float x2 = pos2.x < 0 ? pos2.x * (-1) : pos2.x;
        //    //float y2 = pos2.y < 0 ? pos2.y * (-1) : pos2.y;
        //    //float z2 = pos2.z < 0 ? pos2.z * (-1) : pos2.z;
        //    float a = pos2.x - pos1.x;
        //    a = a < 0 ? a * (-1) : a;
        //    float b = pos2.y - pos1.y;
        //    b = b < 0 ? b * (-1) : b;
        //    float z = pos2.z - pos1.z;
        //    z = z < 0 ? z * (-1) : z;
        //    return FindHypotenuse(z, FindHypotenuse(a, b));
        //}

        //private float FindHypotenuse(float a, float b)
        //{
        //    return (float)Math.Sqrt(a * a + b * b);
        //}

        //void UnturnedPlayerEvents_OnPlayerDead(UnturnedPlayer player, Vector3 position)
        //{

        //}
    }
    //    class Murder
    //    {
    //        UnturnedPlayer _deadPlayer;
    //        EDeathCause _cause;
    //        ELimb _limb;
    //        CSteamID _killer;
    //        public Murder()
    //        {

    //        }
    //    }
}
