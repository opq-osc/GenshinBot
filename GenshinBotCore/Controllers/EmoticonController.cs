﻿using GenshinBotCore.Services.Data;
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
    [YukinoshitaController(Command = "随机表情", MatchMethod = CommandMatchMethod.StartWith, Priority = 4)]
    public class EmoticonController : BotControllerBase
    {
        public EmoticonController(EmoticonSet emoticons)
        {
            this.emoticons = emoticons;
        }

        private readonly EmoticonSet emoticons;

        [FriendText, GroupText]
        public async Task TextMsgHandlerAsync(TextMessage message)
        {
            var cmd = message.Content.Split(' ');
            if (cmd.Length > 1)
            {
                message.Reply(new PictureMessageRequest(await emoticons.GetRandomEmoticonAsync(cmd[1])));
            }
            else
            {
                message.Reply(new PictureMessageRequest(await emoticons.GetRandomEmoticonAsync()));
            }
        }
    }
}
