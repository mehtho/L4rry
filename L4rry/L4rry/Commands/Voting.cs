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
    public struct VoteUnit
    {
        public List<int> optionNo;
        public String voter;
    }

    public class Voting : ModuleBase<SocketCommandContext>
    {
        private static List<VoteUnit> VoteStore = new List<VoteUnit>();
        private static Boolean VoteOn = false;
        private static int OptionCount = 0;
        private static List<String> OptionNames = new List<string>();

        [Command("votestart"), Alias("votestart"), Summary("Start a vote")]
        public async Task VotingCommand([Remainder]string voteString)
        {
            if (!VoteOn)
            {
                var options = voteString.Split(' ').ToList();
                await Context.Channel.SendMessageAsync("Vote Started! Vote with !Vote [option no.]");

                int count = 1;
                foreach (String s in options)
                {
                    OptionNames.Add(s);
                    await Context.Channel.SendMessageAsync(count++ + ". " + s);
                }

                OptionCount = count;
                VoteOn = true;
            }
        }

        [Command("voteend"), Alias("voteend"), Summary("End a vote")]
        public async Task VotingStop()
        {
            if (VoteOn)
            {
                List<int> VoteTotals = new List<int>();

                VoteOn = false;
                await Context.Channel.SendMessageAsync("Vote ended!");

                for (int i = 1; i <= OptionCount; i++)
                {
                    VoteTotals.Add(0);
                }

                foreach (VoteUnit v in VoteStore)
                {
                    foreach (int o in v.optionNo)
                    {
                        VoteTotals[o - 1] += 1;
                    }
                }

                int c = 1;
                foreach (int p in VoteTotals)
                {
                    await Context.Channel.SendMessageAsync(OptionNames[c++ - 1] + ". " + p);
                }
                VoteOn = false;
            }
        }

        [Command("vote"), Alias("vote"), Summary("Vote")]
        public async Task Vote([Remainder]string voteString)
        {
            if (VoteOn)
            {
                VoteUnit dup = HasUserVoted(Context.User.Id.ToString());
                if (!dup.voter.Equals(""))
                {
                    VoteStore.Remove(dup);
                }

                List<int> voteCount = new List<int>();

                List<String> votes = voteString.Split(' ').ToList();
                foreach (String s in votes)
                {
                    int output;
                    if (Int32.TryParse(s, out output))
                    {
                        if (!voteCount.Contains(output))
                        {
                            voteCount.Add(output);
                        }
                    }
                }

                if (voteCount.Count > 0)
                {
                    VoteStore.Add(new VoteUnit { voter = Context.User.Id.ToString(), optionNo = voteCount });
                    await Context.Channel.SendMessageAsync("Vote acknowledged");
                }

                await Context.Channel.SendMessageAsync("Vote invalid");
            }
        }

        private VoteUnit HasUserVoted(String user)
        {
            foreach (VoteUnit v in VoteStore)
            {
                if (v.voter.Equals(user))
                {
                    return v;
                }
            }
            return new VoteUnit {voter="" };
        }
    }
}