using CRUDReactJSNetCore.Application.Helpers;
using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Application.Utils;
using MediatR;
using Serilog;
using FuncionarioDomain = CRUDReactJSNetCore.Domain.Entities.Funcionario;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario
{
    public class InserirFuncionarioRequestHandler : IRequestHandler<InserirFuncionarioRequest, long>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public InserirFuncionarioRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<long> Handle(InserirFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var password = BCrypt.Net.BCrypt.HashPassword(FuncionarioUtils.GeneratePassword());

                var insertFuncionario = new FuncionarioDomain
                {
                    Nome = request.Nome,
                    Active = true,
                    Cargo = request.Cargo,
                    DataNascimento = request.DataNascimento,
                    DataCriacao = DateTime.Now,
                    Senha = EncryptHelper.EncriptarPassword(password),
                    Email = request.Email,
                    Gestor = request.Gestor,
                    Telefone = request.Telefone,
                    Documento = request.Documento,
                };

                await _funcionarioRepository.InserirFuncionario(insertFuncionario);

                return insertFuncionario.Id;

            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao inserir um fucionário: {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
