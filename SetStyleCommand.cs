using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using UnityEngine;

namespace KillReportUI
{
    public class SetStyleCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "Dstyle";
        public string Help => "command to set death UI styles";
        public string Syntax => "/dstyle [style number]";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "rocket.dstyle" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            //long num1 = 637091645802855340;
            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, "Invalid command usage, do: /dstyle [style number]", Color.red);
                return;
            }
            if (!ushort.TryParse(command[0], out ushort number) || number > 2)
            {
                UnturnedChat.Say(caller, "Entered invalid style number. There are only 3 styles available: 0, 1 and 2", Color.red);
                return;
            }
            KIllReport.Instance.Configuration.Instance.StyleNumber = number;
            UnturnedChat.Say(caller, $"Style has been changed to: {number}!");
        }
    }
}
