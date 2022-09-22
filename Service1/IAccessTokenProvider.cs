namespace Service1;

public interface IAccessTokenProvider
{
    Task<string> GetAccessToken();
}