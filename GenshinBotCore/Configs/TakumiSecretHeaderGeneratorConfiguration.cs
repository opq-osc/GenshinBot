using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Configs
{
    public class TakumiSecretHeaderGeneratorConfiguration
    {
        public string AppVersion { get; set; } = string.Empty;

        public int ClientType { get; set; }

        public string Salt { get; set; } = string.Empty;
    }
}
