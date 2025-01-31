using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.GetFuncionarioById;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CRUDReactJSNetCore.Test.Tests
{
    public class FuncionarioQueriesTests
    {
        private readonly HttpClient _client;
        private const string URL_BASE = "http://localhost:9000";

        public FuncionarioQueriesTests()
        {
            _client = new HttpClient()
            {
                BaseAddress = new(URL_BASE),
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        [TestPriority(2)]
        public async Task ShouldListFuncionario_Full_OnePage_50records()
        {
            using var response = await _client.GetAsync($"api/{1}/{50}");

            if (response.IsSuccessStatusCode)
            {
                var strContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<ListFuncionarioResponse>>(strContent);

                Assert.True(result.Any());

            }
            else
                Assert.Fail("Erro ao listar os funcionários");

        }

        [Fact]
        [TestPriority(2)]
        public async Task ShouldListFuncionario_Filtered()
        {
            using var response = await _client.GetAsync($"api/{1}/{50}?filter=Administrador");

            if (response.IsSuccessStatusCode)
            {
                var strContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<ListFuncionarioResponse>>(strContent);

                Assert.True(result.Any());

            }
            else
                Assert.Fail("Erro ao listar os funcionários");
        }

        [Fact]
        [TestPriority(2)]
        public async Task GivenFuncionarioId_ShouldGetFuncionario()
        {
            using var response = await _client.GetAsync($"api?Id={1}");

            if (response.IsSuccessStatusCode)
            {
                var strContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetFuncionarioByIdResponse>(strContent);

                Assert.True(result != null && result?.Id == 1);

            }
            else
                Assert.Fail("Erro ao listar os funcionários");
        }

    }
}
