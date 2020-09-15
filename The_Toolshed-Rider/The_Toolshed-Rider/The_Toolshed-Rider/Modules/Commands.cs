using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json.Serialization;

namespace The_Toolshed_Rider.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        // Basic Text Commands
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }

        [Command("Marco")]
        public async Task Marco()
        {
            await ReplyAsync("Polo");
        }
        
        [Command("Hotel")]
        public async Task Hotel()
        {
            await ReplyAsync("Trivago");
        }
        
        [Command("Fuck")]
        public async Task Fuck()
        {
            await ReplyAsync("You");
        }

        //Kick, ban, unban (mute?)
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage ="You don't have permission to use that command ``ban_member``")]

        public async Task BanMember(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("Select a user to ban");
                return;
            }
            if (reason == null) reason = "Provide a reason for ban";

            await Context.Guild.AddBanAsync(user, 1, reason);

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":white_check_mark: {user.Mention} is now banned\n*Reason* {reason}")
                .WithFooter(footer =>
                {
                    footer
                        .WithText("User ban log")
                        .WithIconUrl("http://www.latechurch.com/wp-content/uploads/2015/08/judges-hammer.jpg");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
            
            ITextChannel logChannel = Context.Client.GetChannel(Log Channel ID goes here) as ITextChannel;
            var EmbedBuilderLog = new EmbedBuilder()
                .WithDescription($":{user.Mention} is now banned\n**Reason** {reason}\n**Moderator** {Context.User.Mention}")
                .WithFooter(footer =>
                {
                    footer
                        .WithText("User ban log")
                        .WithIconUrl("http://www.latechurch.com/wp-content/uploads/2015/08/judges-hammer.jpg");
                });
            Embed embedLog = EmbedBuilderLog.Build();
            await logChannel.SendMessageAsync(embed: embedLog);
        }
    }
}