using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListCargos;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListGestores;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CRUDReactJSNetCore.Test.Tests
{
    public class ListasTests
    {
        private readonly HttpClient _client;
        private const string URL_BASE = "http://localhost:9000";

        public ListasTests()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(URL_BASE),
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [TestPriority(1)]
        public async Task ListGestor_ShouldReturnList_ForLevels(int level)
        {
            // Given
            var urlListGestor = $"api/gestores/{level}";
            var response = await _client.GetAsync(urlListGestor);

            // When
            if (response.IsSuccessStatusCode)
            {
                // Given
                var str = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<ListGestoresResponse>>(str);

                // When

                if (level == 1)
                {
                    // Then
                    Assert.True(result.Any(x => x.GestorId == 1));
                }
                else
                {
                    if (result.Any()) // When
                    {
                        // Then
                        Assert.True(!result.Any(x => x.LevelCargo >= level));
                    }

                }

            }
            else // Then
                Assert.Fail("Erro de requisição a API");

        }

        [Fact]
        [TestPriority(1)]
        public async Task ListCargos_ShouldReturnList()
        {
            // Given
            var response = await _client.GetAsync("api/cargos");

            // When
            if (response.IsSuccessStatusCode)
            {
                // Given
                var str = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<ListCargosResponse>>(str);

                //When
                if (result != null)
                {
                    Assert.True(result.Any());
                }
                else // Then
                    Assert.Fail("Nenhum cargo Carregado");

                // Then
            }
            else // Then
                Assert.Fail("Erro de requisição a API");
        }
    }
}