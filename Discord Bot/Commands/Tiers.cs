using Discord_Bot.Customer;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    public class Tiers : BaseCommandModule
    {
        public string parsedTest { get; set; }

        [Command("tier")]
        [Description(" Shows your Tiers! Ranging from 1 - 4")] // this will be displayed to tell users what this command does when they invoke help
        [Aliases("tiers")]
        public async Task customerTier(CommandContext ctx)
        {
            var hello = new Data(ctx.Member.Id.ToString());

            await ctx.Channel.SendMessageAsync("Your actual ID: " + ctx.User.Id.ToString() + "\n" +
                                               "Your fake ID: " + hello.getID().ToString() + "\n" +
                                               "Your tier: " + hello.getTier().ToString() + "\n" +
                                               "Spendings: $" + hello.getSpendings().ToString());

        }


        // /add @uwu 10
        // <@236412553968877569>
        //   236412553968877569 
        // 2
        // 0123456789

        [Command("add")]
        public async Task addSpendings(CommandContext ctx, string whoTF, float amount)
        {
            //await ctx.Channel.SendMessageAsync(whoTF);

            whoTF = removeUUIDChar(whoTF);

            Console.WriteLine(whoTF);

            var hello2 = new Data(ctx.Member.Id.ToString());

            await ctx.Channel.SendMessageAsync("Test UUID: " + whoTF);

            await ctx.Channel.SendMessageAsync("Adding this amount: $" + amount.ToString());

            hello2.addSpendings(whoTF, amount);

            await ctx.Channel.SendMessageAsync("Your new spendings: $" + hello2.getSpendings());
        }

        public string removeUUIDChar(string unparsed) // Removes the "<@" and ">" from the string
        {
            string parsed = unparsed.Remove(0, 2);
            parsed = parsed.Remove(parsed.Length - 1, 1);
            return parsed;
        }

        [Command("uuid")]
        public async Task uniqueID(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(ctx.Member.Id.ToString());
        }
    }
}
