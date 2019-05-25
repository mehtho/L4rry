using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4rry.Commands
{
    public class IanCommand : ModuleBase<SocketCommandContext>
    {
        [Command("matt is tall"), Alias("matt"), Summary("Matts height")]
        public async Task MattCommand()
        {
            await Context.Channel.SendMessageAsync("162.9cm lmao");
        }
        [Command("shark"), Alias("s"), Summary("baby shark")]
        public async Task SharkCommand()
        {
            await Context.Channel.SendMessageAsync("https://imgur.com/gallery/FHOgmxG");
        }
    }
}
