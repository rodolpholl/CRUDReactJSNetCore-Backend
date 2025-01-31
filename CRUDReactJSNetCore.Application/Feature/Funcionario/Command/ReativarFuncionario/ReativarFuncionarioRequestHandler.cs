using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ReativarFuncionario
{
    public class ReativarFuncionarioRequestHandler : IRequestHandler<ReativarFuncionarioRequest, bool>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ReativarFuncionarioRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<bool> Handle(ReativarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var funcionario = await _funcionarioRepository.GetFuncionarioById(request.FuncionarioId);

                funcionario.Active = true;
                funcionario.DataDesativacao = null;
                funcionario.DataReativacao = DateTime.Now;

                await _funcionarioRepository.AlterarFuncionario(funcionario);

                return true;
            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao Reativar o fucionário com Id '{request.FuncionarioId}': {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
