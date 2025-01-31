namespace DotnetComp.Clients
{
    public interface IRunescapeClient
    {
        Task<HttpResponseMessage> GetPlayerHiscoreAsync(string name);
    }

    public class RunescapeClient(IHttpClientFactory httpClientFactory) : IRunescapeClient
    {
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;

        public async Task<HttpResponseMessage> GetPlayerHiscoreAsync(string name)
        {
            using HttpClient client = httpClientFactory.CreateClient("RunescapeClient");
            var url = $"m=hiscore_oldschool/index_lite.ws?player={name}";
            var response = await client.GetAsync(url);
            return response;
        }
    }
}
