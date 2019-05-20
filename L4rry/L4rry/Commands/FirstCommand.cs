using Discord.Commands;
using L4rry.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4rry.Commands
{
    public class FirstCommand : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("hello"), Summary("Hello world command")]
        public async Task HelloCommand()
        { 
            await Context.Channel.SendMessageAsync("Hello there");

        }
    }
}
