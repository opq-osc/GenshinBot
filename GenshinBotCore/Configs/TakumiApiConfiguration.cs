namespace GenshinBotCore.Configs
{
    /// <summary>
    /// 米游社API配置
    /// </summary>
    public class TakumiApiConfiguration
    {
        /// <summary>
        /// API基地址
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Token获取URL
        /// </summary>
        public string LoginTicketUrl { get; set; } = string.Empty;

        /// <summary>
        /// 游戏帐户获取URL
        /// </summary>
        public string GameAccountsUrl { get; set; } = string.Empty;

        /// <summary>
        /// 深渊战绩URL
        /// </summary>
        public string SpiralAbyssUrl { get; set; } = string.Empty;

        /// <summary>
        /// 每日任务卡片URL
        /// </summary>
        public string DailyNoteUrl { get; set; } = string.Empty;

        /// <summary>
        /// 主页信息URL
        /// </summary>
        public string IndexUrl { get; set; } = string.Empty;
    }
}
