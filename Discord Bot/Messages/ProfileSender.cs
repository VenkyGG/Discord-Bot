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

namespace Discord_Bot.Messages
{
    class ProfileSender : BaseCommandModule
    {
        public async static void yourProfile(CommandContext senderContext)
        {
            string sendersID = senderContext.Member.Id.ToString();

            var yourEmbededProfile = new DiscordEmbedBuilder
            {
                Color = new DiscordColor("#00ff6b"),

                Title = "**Your Profile:**",

                Description = "Showing information for user: **@" + senderContext.Member.Username.ToString() + "**",

                ThumbnailUrl = senderContext.Member.AvatarUrl
            };

            yourEmbededProfile.AddField("Tier:", Data.Instance.getTier(sendersID).ToString(), false);

            yourEmbededProfile.AddField("Total Spendings:", "$" + Data.Instance.getSpendings(sendersID).ToString(), false);

            await senderContext.Channel.SendMessageAsync(embed: yourEmbededProfile);
        }

        public async static void otherProfile(CommandContext senderContext, DiscordUser otherDiscordUser)
        {
            string requestedID = otherDiscordUser.Id.ToString();

            var otherEmbededProfile = new DiscordEmbedBuilder
            {
                Color = new DiscordColor("#00ff6b"),

                Title = "**" + otherDiscordUser.Username + "'s Profile:**",

                Description = "Showing information for user: **@" + otherDiscordUser.Username + "**",

                ThumbnailUrl = otherDiscordUser.AvatarUrl
            };

            otherEmbededProfile.AddField("Tier:", Data.Instance.getTier(requestedID).ToString(), false);

            otherEmbededProfile.AddField("Total Spendings:", "$" + Data.Instance.getSpendings(requestedID).ToString(), false);

            await senderContext.Channel.SendMessageAsync(embed: otherEmbededProfile);
        }

        public async static void updatingProfile(CommandContext senderContext, DiscordUser customerDiscord, float amount, int AddRemoveSet)
        {
            string customerID = customerDiscord.Id.ToString();

            if (AddRemoveSet == 1)
            {
                Data.Instance.addSpendings(customerID, amount);
            }
            else if (AddRemoveSet == 2)
            {
                Data.Instance.removeSpendings(customerID, amount);
            }
            else
            {
                Data.Instance.setSpendings(customerID, amount);
            }

            var customerEmbededProfile = new DiscordEmbedBuilder
            {
                Color = new DiscordColor("#00ff6b"),

                Title = "**Updating " + customerDiscord.Username + "'s Profile:**",

                Description = "Showing updated information for user: **@" + customerDiscord.Username + "**",

                ThumbnailUrl = customerDiscord.AvatarUrl
            };

            customerEmbededProfile.AddField("Tier:", Data.Instance.getTier(customerID).ToString(), false);

            customerEmbededProfile.AddField("Total Spendings:", "$" + Data.Instance.getSpendings(customerID).ToString(), false);

            await senderContext.Channel.SendMessageAsync(embed: customerEmbededProfile);
        }
    }
}