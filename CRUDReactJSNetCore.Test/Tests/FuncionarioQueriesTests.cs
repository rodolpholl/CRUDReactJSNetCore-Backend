using Bogus;
using System.Net.Http.Headers;

namespace CRUDReactJSNetCore.Test.Tests
{
    public class FuncionarioQueriesTests
    {
        private readonly HttpClient _client;
        private const string URL_BASE = "http://localhost:9000";
        private readonly Faker _faker = new("pt_BR");

        public FuncionarioQueriesTests()
        {
            _client = new HttpClient()
            {
                BaseAddress = new(URL_BASE),
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }





    }
}
