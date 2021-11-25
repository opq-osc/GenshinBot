using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Models
{
    public interface IApiResponse<T>
    {
        bool IsSuccess { get; }
        Type ApiDataType { get; }
        T? Payload { get; }
    }
}
