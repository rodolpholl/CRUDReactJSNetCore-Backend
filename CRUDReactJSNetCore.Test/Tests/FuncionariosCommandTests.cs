using Bogus;
using Bogus.Extensions.Brazil;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListCargos;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario;
using CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListGestores;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CRUDReactJSNetCore.Test.Tests
{
    public class FuncionariosCommandTests
    {
        private readonly HttpClient _client;
        private const string URL_BASE = "http://localhost:9000";
        private readonly Faker _faker = new("pt_BR");

        public FuncionariosCommandTests()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(URL_BASE),
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Insert Funcionario

        private static IList<InserirFuncionarioRequest> GenerateInsertSource()
        {
            IList<ListCargosResponse> listCargos = null;
            IList<ListGestoresResponse> listGestores = null;


            HttpClient client = new()
            {
                BaseAddress = new Uri(URL_BASE),
            };

            // Carregando os cargos
            var response = Task.Run(() => client.GetAsync("api/cargos")).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var strListCargos = Task.Run(() => response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();
                listCargos = JsonConvert.DeserializeObject<IList<ListCargosResponse>>(strListCargos);
            }

            //Carregando os Gestores
            response = Task.Run(() => client.GetAsync("api/gestores/6")).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var strListGestores = Task.Run(() => response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();
                listGestores = JsonConvert.DeserializeObject<IList<ListGestoresResponse>>(strListGestores);
            }


            var result = new List<InserirFuncionarioRequest>();
            foreach (var gestorFuncionario in listGestores)
            {
                Faker faker = new("pt_BR");
                var cargoFuncionario = listCargos.FirstOrDefault(x => x.Level > gestorFuncionario.LevelCargo);
                if (cargoFuncionario == null && gestorFuncionario.LevelCargo == 5)
                    cargoFuncionario = listCargos.FirstOrDefault(x => x.Level == 5);



                var insert = new InserirFuncionarioRequest
                {
                    DataNascimento = faker.Date.Past(19, DateTime.Now.AddYears(-65)),
                    CargoId = cargoFuncionario.Id,
                    GestorId = gestorFuncionario.GestorId,
                    Documento = faker.Person.Cpf(false),
                    Email = faker.Person.Email,
                    Nome = faker.Person.FullName,
                };

                var randomPhones = (new Random()).Next(1, 5);
                var listNumber = new List<string>();
                for (var i = 0; i < randomPhones; i++)
                    listNumber.Add(faker.Phone.PhoneNumber("##########"));

                insert.Telefone = listNumber.ToArray();
                result.Add(insert);

            }

            return result;


        }

        [Fact]
        [TestPriority(1)]
        public async Task GivenFuncionario_ShouldInsertFuncionario()
        {
            var listIsert = GenerateInsertSource();

            if (!listIsert.Any())
                Assert.Fail("Nenhum Funcionario para inclusão");

            var urlListGestor = $"api";
            bool valid = true;

            foreach (var insert in listIsert)
            {
                var
                // Given
                strContent = JsonConvert.SerializeObject(insert);
                var content = new StringContent(strContent, Encoding.UTF8, "application/json");



                using var response = await _client.PostAsync(urlListGestor, content);



                // When
                if (response.IsSuccessStatusCode)
                {
                    // Then
                    var str = new StreamReader(response.Content.ReadAsStream());
                    var strResponse = str.ReadToEnd(); //await response.Content.ReadAsStringAsync();

                    // When
                    // Given
                    if (long.TryParse(strResponse, out long idFuncionario))
                    {

                        // Then
                        valid = idFuncionario > 1;
                    }
                    else // Then
                        Assert.Fail("Falha na inclusão do funcionário");

                }
                else // Then
                {
                    var str = new StreamReader(response.Content.ReadAsStream());
                    var strResponse = str.ReadToEnd();

                    Assert.Fail($"Falha na inclusão do funcionário: {strResponse}");
                }
            }

            Assert.True(valid);

        }

        #endregion Insert Funcionario


        #region Alterar Funcionario

        [Fact]
        [TestPriority(3)]
        public async Task GivenFuncionario_ShouldUpdateFuncionario()
        {
            // Giver
            var faker = new Faker("pt_BR");

            var response = await _client.GetAsync("api/1/50");
            var message = response.EnsureSuccessStatusCode();
            var strContent = await message.Content.ReadAsStringAsync();
            var listCliente = JsonConvert.DeserializeObject<IList<ListFuncionarioResponse>>(strContent);
            var randomIndex = (new Random()).Next(1, listCliente.Count() - 1);
            var funcionarioUpdate = listCliente[randomIndex];

            response = await _client.GetAsync("api/cargos");
            message = response.EnsureSuccessStatusCode();
            strContent = await message.Content.ReadAsStringAsync();
            var listCargos = JsonConvert.DeserializeObject<IList<ListCargosResponse>>(strContent);
            listCargos = listCargos?.Where(x => x.Name != funcionarioUpdate.Cargo && x.Level > 0).ToList();
            randomIndex = (new Random()).Next(0, listCargos.Count() - 1);
            var cargoUpdate = listCargos[randomIndex];

            response = await _client.GetAsync($"api/gestores/{cargoUpdate.Level}");
            message = response.EnsureSuccessStatusCode();
            strContent = await message.Content.ReadAsStringAsync();
            var listGestores = JsonConvert.DeserializeObject<IList<ListGestoresResponse>>(strContent);
            listGestores = listGestores.Where(x => x.LevelCargo > 0).ToList();
            var gestorUpdate = listGestores.FirstOrDefault(x => x.LevelCargo < cargoUpdate.Level);

            var randomPhones = (new Random()).Next(1, 5);
            var listNumber = new List<string>();
            for (var i = 0; i < randomPhones; i++)
                listNumber.Add(faker.Phone.PhoneNumber("##########"));

            // When
            var alteracao = new AlterarFuncionarioRequest
            {
                CargoId = cargoUpdate.Id,
                Email = faker.Person.Email,
                Nome = faker.Person.FullName,
                Documento = faker.Person.Cpf(false),
                Telefone = listNumber.ToArray(),
                GestorId = gestorUpdate.GestorId,
                DataNascimento = faker.Person.DateOfBirth,
            };

            strContent = JsonConvert.SerializeObject(alteracao);
            var content = new StringContent(strContent, Encoding.UTF8, "application/json");
            response = await _client.PutAsync($"api/{funcionarioUpdate.Id}", content);
            message = response.EnsureSuccessStatusCode();
            strContent = await message.Content.ReadAsStringAsync();


            // Then
            Assert.True(funcionarioUpdate.Id == long.Parse(strContent));

        }

        [Fact]
        [TestPriority(4)]
        public async Task GivenFuncionario_ShouldDeactivateFuncionario_Then_ReactivarFuncionario()
        {
            // Giver
            var faker = new Faker("pt_BR");

            var response = await _client.GetAsync("api/1/50");
            var message = response.EnsureSuccessStatusCode();
            var strContent = await message.Content.ReadAsStringAsync();
            var listCliente = JsonConvert.DeserializeObject<IList<ListFuncionarioResponse>>(strContent);
            var randomIndex = (new Random()).Next(1, listCliente.Count() - 1);
            var funcionario = listCliente[randomIndex];

            // When
            response = await _client.PatchAsync($"api/{funcionario.Id}/desativar", null);
            message = response.EnsureSuccessStatusCode();
            strContent = await message.Content.ReadAsStringAsync();

            // Then
            bool assert = bool.Parse(strContent);

            if (assert)
            {
                // When
                response = await _client.PatchAsync($"api/{funcionario.Id}/reativar", null);
                message = response.EnsureSuccessStatusCode();
                strContent = await message.Content.ReadAsStringAsync();

                // Then
                assert = bool.Parse(strContent);
            }

            Assert.True(assert);
        }

        [Fact]
        [TestPriority(5)]
        public async Task GivenFuncionario_ShouldDeleteFuncionario()
        {
            // Giver
            var faker = new Faker("pt_BR");

            var response = await _client.GetAsync("api/1/50");
            var message = response.EnsureSuccessStatusCode();
            var strContent = await message.Content.ReadAsStringAsync();
            var listCliente = JsonConvert.DeserializeObject<IList<ListFuncionarioResponse>>(strContent);
            var randomIndex = (new Random()).Next(1, listCliente.Count() - 1);
            var funcionario = listCliente[randomIndex];

            // When
            response = await _client.DeleteAsync($"api/{funcionario.Id}");
            message = response.EnsureSuccessStatusCode();
            strContent = await message.Content.ReadAsStringAsync();

            Assert.True(bool.Parse(strContent));
        }

        #endregion Alterar Funcionario

    }
}
