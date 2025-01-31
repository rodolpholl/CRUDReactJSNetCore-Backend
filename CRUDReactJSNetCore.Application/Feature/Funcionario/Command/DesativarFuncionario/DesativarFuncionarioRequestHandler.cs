using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.DesativarFuncionario
{
    public class DesativarFuncionarioRequestHandler : IRequestHandler<DesativarFuncionarioRequest, bool>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public DesativarFuncionarioRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<bool> Handle(DesativarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var funcionario = await _funcionarioRepository.GetFuncionarioById(request.FuncionarioId);

                funcionario.Active = false;
                funcionario.DataDesativacao = DateTime.Now;
                funcionario.DataReativacao = null;

                await _funcionarioRepository.AlterarFuncionario(funcionario);

                return true;

            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao Desativar o fucionário com Id '{request.FuncionarioId}': {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
