namespace GenshinBotCore.Models
{
    public interface IApiResponse<T>
    {
        bool IsSuccess { get; }
        Type ApiDataType { get; }
        T? Payload { get; }
    }
}
