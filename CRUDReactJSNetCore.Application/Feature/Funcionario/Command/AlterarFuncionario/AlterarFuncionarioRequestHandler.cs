using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario
{
    public class AlterarFuncionarioRequestHandler : IRequestHandler<AlterarFuncionarioRequest, long>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public AlterarFuncionarioRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<long> Handle(AlterarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var updateFuncionario = await _funcionarioRepository.GetFuncionarioById(request.Id);

                updateFuncionario.Nome = request.Nome;
                updateFuncionario.Email = request.Email;
                updateFuncionario.Telefone = request.Telefone;
                updateFuncionario.Cargo = request.Cargo;
                updateFuncionario.Gestor = request.Gestor;
                updateFuncionario.Documento = request.Documento;
                updateFuncionario.DataNascimento = request.DataNascimento;

                await _funcionarioRepository.AlterarFuncionario(updateFuncionario);

                return updateFuncionario.Id;

            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao alterar o fucionário com Id '{request.Id}': {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
