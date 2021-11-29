namespace GenshinBotCore.Services
{
    public interface ISecretHeaderGenerator
    {
        /// <summary>
        /// 生成机密请求头
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="query">查询字符串</param>
        /// <returns></returns>
        IDictionary<string, string> GenerateSecretHeader(int userId, string query);
    }
}
