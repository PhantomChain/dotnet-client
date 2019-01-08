using System;
using System.Net.Http;
using PhantomChain.Client.API;

namespace PhantomChain.Client
{
    public interface IConnection
    {
        Api Api { get; }
        HttpClient Client { get; }
    }

    public sealed class Connection : IConnection
    {
        public HttpClient Client { get; }

        public Api Api { get; }

        public Connection(string host) : this(CreateClient(host)) { }

        public Connection(HttpClient client)
        {
            Api = new Api(client);

            Client = client;
            Client.DefaultRequestHeaders.TryAddWithoutValidation("API-Version", "2");
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        static HttpClient CreateClient(string host)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(host)
            };

            return client;
        }
    }
}
