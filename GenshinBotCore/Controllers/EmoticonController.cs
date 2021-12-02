using GenshinBotCore.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Data.OpqApi;

namespace GenshinBotCore.Controllers
{
    [StartRoute(Command = "随机表情", Priority = 4)]
    public class EmoticonController : BotControllerBase
    {
        public EmoticonController(EmoticonSet emoticons)
        {
            this.emoticons = emoticons;
        }

        private readonly EmoticonSet emoticons;

        [FriendText, GroupText]
        public async Task TextMsgHandlerAsync()
        {
            var cmd = (this.Message as TextMessage)?.Content.Split(' ') ?? throw new NullReferenceException();
            if (cmd.Length > 1)
            {
                this.Message.Reply(new PictureMessageRequest(await emoticons.GetRandomEmoticonAsync(cmd[1])));
            }
            else
            {
                this.Message.Reply(new PictureMessageRequest(await emoticons.GetRandomEmoticonAsync()));
            }
        }
    }
}
