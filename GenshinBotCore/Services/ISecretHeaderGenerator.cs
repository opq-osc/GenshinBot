using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services
{
    public interface ISecretHeaderGenerator
    {
        IDictionary<string, string> GenerateSecretHeader(string query);
    }
}
