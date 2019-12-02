//using System;
//using Rocket.Core.Plugins;
//using Rocket.Unturned.Events;
//using Rocket.Unturned.Player;
//using SDG.Unturned;
//using Steamworks;
//using UnityEngine;

//namespace KillReportUI
//{
//    public class KIllReport : RocketPlugin<KIllReportConfiguration>
//    {
//        internal static KIllReport Instance;
//        public const int ProductID = 210; // Read #how-to-publish to see how you obtain a Product ID
//        public System.Version ProductVersion = new System.Version("1.0.0.1"); //Keep it the same when uploading to website!

//        protected override void Load()
//        {
//            if(Configuration.Instance.StyleNumber > 2)
//            {
//                Rocket.Core.Logging.Logger.LogWarning("StyleNumber in config cannot be more than 2. Only 3 styles available: 0(default), 1 and 2. Setting to default...");
//                Configuration.Instance.StyleNumber = 0;
//            }
//            Instance = this;
//            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
//        }
//        protected override void Unload()
//        {
//            UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
//            foreach (var steamPlayer in Provider.clients)
//                UnturnedPlayer.FromSteamPlayer(steamPlayer).Events.OnRevive -= Events_OnRevive;
//            //UnturnedPlayerEvents.OnPlayerDead -= UnturnedPlayerEvents_OnPlayerDead;
//        }

//        void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
//        {
//            if (cause != EDeathCause.GUN && cause != EDeathCause.MELEE)
//                return;
//            Player killer = PlayerTool.getPlayer(murderer);
//            player.Events.OnRevive += Events_OnRevive;
//            EffectManager.sendUIEffect((ushort)(Configuration.Instance.StyleNumber + 8110), 30, player.CSteamID, false);
//            EffectManager.sendUIEffectText(30, player.CSteamID, false, "Killer", $"{killer.channel.owner.playerID.characterName}");
//            EffectManager.sendUIEffectText(30, player.CSteamID, false, "Weapon", $"{killer.equipment.asset.itemName}");
//            EffectManager.sendUIEffectText(30, player.CSteamID, false, "Distance", $"{Math.Round(Vector3.Distance(player.Position, killer.transform.position), 2)} M");
//            EffectManager.sendUIEffectText(30, player.CSteamID, false, "EnemyHealth", $"{killer.life.health} HP");
//            //if(killer != null)
//            //{

//            //}
//            //else
//            //{
//            //    switch (cause)
//            //    {
//            //        case EDeathCause.
//            //        default:
//            //            break;
//            //    }
//            //}

//            //EffectManager.sendUIEffectText(30, player.CSteamID, false, "Weapon", $"{(killer != null ? killer.equipment.asset.itemName : $"")}");
//            //EffectManager.sendUIEffectText(30, player.CSteamID, false, "Distance", $"{(killer != null ? Math.Round(Vector3.Distance(player.Position, killer.transform.position), 2).ToString() : "1")} M");
//            //EffectManager.sendUIEffectText(30, player.CSteamID, false, "EnemyHealth", $"{killer.life.health} HP");
//        }

//        void Events_OnRevive(UnturnedPlayer player, Vector3 position, byte angle)
//        {
//            EffectManager.askEffectClearByID(8110, player.CSteamID);
//            EffectManager.askEffectClearByID(8111, player.CSteamID);
//            EffectManager.askEffectClearByID(8112, player.CSteamID);
//        }


//        //private float FindDistance(Vector3 pos1, Vector3 pos2)
//        //{
//        //    //float x1 = pos1.x < 0 ? pos1.x * (-1) : pos1.x;
//        //    //float y1 = pos1.y < 0 ? pos1.y * (-1) : pos1.y;
//        //    //float z1 = pos1.z < 0 ? pos1.z * (-1) : pos1.z;
//        //    //float x2 = pos2.x < 0 ? pos2.x * (-1) : pos2.x;
//        //    //float y2 = pos2.y < 0 ? pos2.y * (-1) : pos2.y;
//        //    //float z2 = pos2.z < 0 ? pos2.z * (-1) : pos2.z;
//        //    float a = pos2.x - pos1.x;
//        //    a = a < 0 ? a * (-1) : a;
//        //    float b = pos2.y - pos1.y;
//        //    b = b < 0 ? b * (-1) : b;
//        //    float z = pos2.z - pos1.z;
//        //    z = z < 0 ? z * (-1) : z;
//        //    return FindHypotenuse(z, FindHypotenuse(a, b));
//        //}

//        //private float FindHypotenuse(float a, float b)
//        //{
//        //    return (float)Math.Sqrt(a * a + b * b);
//        //}

//        //void UnturnedPlayerEvents_OnPlayerDead(UnturnedPlayer player, Vector3 position)
//        //{

//        //}
//    }
//    //    class Murder
//    //    {
//    //        UnturnedPlayer _deadPlayer;
//    //        EDeathCause _cause;
//    //        ELimb _limb;
//    //        CSteamID _killer;
//    //        public Murder()
//    //        {

//    //        }
//    //    }
//}
