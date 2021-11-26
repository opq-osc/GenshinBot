using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Configs
{
    public class SecretManagerConfiguration
    {
        public string SymmetricKey { get; set; } = null!;
        public string SymmetricSalt { get; set; } = null!;
        public string HashSalt { get; set; } = null!;
    }
}
