using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenshinBotCore.Entities
{
    public class UserSecret
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
