using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBotCore.Entities
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        [Required]
        public long QQ { get; set; }

        /// <summary>
        /// 米游社Id
        /// </summary>
        public string MihoyoId { get; set; } = string.Empty;

        /// <summary>
        /// 原神Uid
        /// </summary>
        public string GenshinUid { get; set; } = string.Empty;

        /// <summary>
        /// 服务器Id
        /// </summary>
        public string ServerId { get; set; } = string.Empty;
    }
}
