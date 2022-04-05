using System.Text.Json.Serialization;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 每日便签
    /// </summary>
    public class DailyNote
    {
        /// <summary>
        /// 当前树脂数量
        /// </summary>
        [JsonPropertyName("current_resin")]
        public int CurrentResin { get; set; }

        /// <summary>
        /// 最大树脂数量
        /// </summary>
        [JsonPropertyName("max_resin")]
        public int MaxResin { get; set; }

        /// <summary>
        /// 树脂回满所需时间
        /// </summary>
        [JsonPropertyName("resin_recovery_time")]
        public string ResinRecoveryTime { get; set; } = null!;

        /// <summary>
        /// 已完成每日任务数量
        /// </summary>
        [JsonPropertyName("finished_task_num")]
        public int FinishedTaskNum { get; set; }

        /// <summary>
        /// 总的每日任务数量
        /// </summary>
        [JsonPropertyName("total_task_num")]
        public int TotalTaskNum { get; set; }

        /// <summary>
        /// 是否领取每日任务额外奖励
        /// </summary>
        [JsonPropertyName("is_extra_task_reward_received")]
        public bool IsExtraTaskRewardReceived { get; set; } = false;

        /// <summary>
        /// 剩余周本次数
        /// </summary>
        [JsonPropertyName("remain_resin_discount_num")]
        public int RemainResinDiscountNum { get; set; }

        /// <summary>
        /// 周本总次数
        /// </summary>
        [JsonPropertyName("resin_discount_num_limit")]
        public int ResinDiscountNumLimit { get; set; }

        /// <summary>
        /// 当前探索派遣数量
        /// </summary>
        [JsonPropertyName("current_expedition_num")]
        public int CurrentExpeditionNum { get; set; }

        /// <summary>
        /// 最大探索派遣数量
        /// </summary>
        [JsonPropertyName("max_expedition_num")]
        public int MaxExpeditionNum { get; set; }

        /// <summary>
        /// 探索派遣详情
        /// </summary>
        [JsonPropertyName("expeditions")]
        public IEnumerable<ExpeditionInfo> Expeditions { get; set; } = null!;

        /// <summary>
        /// 洞天宝钱
        /// </summary>
        [JsonPropertyName("current_home_coin")]
        public int CurrentHomeCoin { get; set; }

        /// <summary>
        /// 最大洞天宝钱
        /// </summary>
        [JsonPropertyName("max_home_coin")]
        public int MaxHomeCoin { get; set; }

        /// <summary>
        /// 洞天宝钱剩余时间
        /// </summary>
        [JsonPropertyName("home_coin_recovery_time")]
        public string HomeCoinRecoveryTime { get; set; } = null!;

        /// <summary>
        /// 参量质变仪
        /// </summary>
        [JsonPropertyName("transformer")]
        public TransformerInfo TransformerInfo { get; set; } = null!;
    }

    /// <summary>
    /// 探索派遣信息
    /// </summary>
    public class ExpeditionInfo
    {
        /// <summary>
        /// 角色头像
        /// </summary>
        [JsonPropertyName("avatar_side_icon")]
        public Uri CharacterAvatar { get; set; } = null!;

        /// <summary>
        /// 剩余时间
        /// </summary>
        [JsonPropertyName("remained_time")]
        public string RemainedTime { get; set; } = null!;

        /// <summary>
        /// 状态
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
