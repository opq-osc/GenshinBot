using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Configs
{
    public class TakumiApiConfiguration
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string LoginTicketUrl { get; set; } = string.Empty;
        public string GameAccountsUrl { get; set; } = string.Empty;
        public string SpiralAbyssUrl { get; set; } = string.Empty;
        public string DailyNoteUrl { get; set; } = string.Empty;
        public string IndexUrl { get; set; } = string.Empty;
    }
}
