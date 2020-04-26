using Discord_Bot.Customer;
using Discord_Bot.Messages;
using DSharpPlus;
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
        [Command("profile"), Aliases("viewprofile"), Description(" Displays your profile!")]
        public async Task customerTier(CommandContext Context)
        {
            await Context.Message.DeleteAsync();

            ProfileSender.yourProfile(Context);
        }

        [Command("profile"), Description(" Displays your profile!")]
        public async Task customerTier2(CommandContext Context, DiscordUser requestedUser)
        {
            await Context.Message.DeleteAsync();

            if (Context.Member.Id.ToString() == requestedUser.Id.ToString())
            {
                ProfileSender.yourProfile(Context);
            }
            else
            {
                ProfileSender.otherProfile(Context, requestedUser);
            }
        }

        [Command("add")]
        [Hidden]
        [RequireRoles(RoleCheckMode.Any, "Support Team")]
        public async Task addSpendings(CommandContext Context, DiscordUser customerDiscord, float amountToAdd)
        {
            await Context.Message.DeleteAsync();

            //ProfileSender.otherProfile(Context, customerDiscord);

            ProfileSender.updatingProfile(Context, customerDiscord, amountToAdd, 1);
        }

        [Command("remove")]
        [Hidden]
        [RequireRoles(RoleCheckMode.Any, "Support Team")]
        public async Task removeSpendings(CommandContext Context, DiscordUser customerDiscord, float amountToAdd)
        {
            await Context.Message.DeleteAsync();

            //ProfileSender.otherProfile(Context, customerDiscord);

            ProfileSender.updatingProfile(Context, customerDiscord, amountToAdd, 2);
        }

        [Command("set")]
        [Hidden]
        [RequireRoles(RoleCheckMode.Any, "Support Team")]
        public async Task setSpendings(CommandContext Context, DiscordUser customerDiscord, float amountToSet)
        {
            await Context.Message.DeleteAsync();

            ProfileSender.updatingProfile(Context, customerDiscord, amountToSet, 3);
        }
    }
}
