using Bogus;
using CRUDReactJSNetCore.Application.Feature.Auth.Command.AlterarPassword;
using CRUDReactJSNetCore.Application.Feature.Auth.Command.EfetuarLogin;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CRUDReactJSNetCore.Test.Tests
{
    public class AutenticacaoCommandTests
    {
        private readonly HttpClient _client;
        private const string URL_BASE = "http://localhost:9001";
        private readonly Faker _faker = new("pt_BR");

        public AutenticacaoCommandTests()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(URL_BASE),
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        [Fact]
        [TestPriority(2)]
        public async Task A_GivenAdminLoginData_ThenLoggedIn()
        {
            // Given
            var urlEfetuarLogin = "api/login";
            var strRequest = JsonConvert.SerializeObject(new EfetuarLoginCommand()
            {
                Email = "admin@admin.com.br",
                Password = "Admin@123"
            });
            var content = new StringContent(strRequest, Encoding.UTF8, "application/json");
            using var response = await _client.PostAsync(urlEfetuarLogin, content);

            // When
            if (response.IsSuccessStatusCode)
            {
                var autenticaton = JsonConvert.DeserializeObject<EfetuarLoginResponse>((await response.Content.ReadAsStringAsync()));
                // Then
                Assert.True(autenticaton.UserId == 1);
            }
            else
                Assert.Fail("Erro de autenticação");
        }

        [Fact]
        [TestPriority(3)]
        public async Task B_GivenAdminAlterData_ThenChangeAutenticationInfo()
        {
            // Given
            var urlAlterarDadosAutenticacao = "api/alterar-password";
            AlterarPasswordCommand altFuncionario = new()
            {
                Email = "admin@admin.com.br",
                SenhaAtual = "Admin@123",
                NovaSenha = "Admin@1234",
                ConfirmarSenha = "Admin@1234"
            };

            var content = new StringContent(JsonConvert.SerializeObject(altFuncionario), Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync(urlAlterarDadosAutenticacao, content);

            // When
            if (response.IsSuccessStatusCode)
            {
                var alteracao = JsonConvert.DeserializeObject<bool>((await response.Content.ReadAsStringAsync()));
                // Then

                if (alteracao)
                {
                    altFuncionario = new()
                    {
                        Email = "admin@admin.com.br",
                        SenhaAtual = "Admin@1234",
                        NovaSenha = "Admin@123",
                        ConfirmarSenha = "Admin@123"
                    };
                    content = new StringContent(JsonConvert.SerializeObject(altFuncionario), Encoding.UTF8, "application/json");
                    response = await _client.PostAsync(urlAlterarDadosAutenticacao, content);
                    response.EnsureSuccessStatusCode();
                    alteracao = JsonConvert.DeserializeObject<bool>((await response.Content.ReadAsStringAsync()));

                    Assert.True(alteracao);
                }
            }
            else
                Assert.Fail("Erro de autenticação");


        }
    }
}
