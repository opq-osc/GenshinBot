using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBotCore.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public long QQ { get; set; }

        public string MihoyoId { get; set; } = string.Empty;

        public string GenshinUid { get; set; } = string.Empty;
    }
}
