using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ExcluirFuncionario
{
    public class ExcluirFuncionarioRequestHandler : IRequestHandler<ExcluirFuncionarioRequest, bool>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ExcluirFuncionarioRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<bool> Handle(ExcluirFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var funcionario = await _funcionarioRepository.GetFuncionarioById(request.FuncionarioId);

                await _funcionarioRepository.ExcluirFuncionario(funcionario);

                return true;

            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao Excluir o fucionário com Id '{request.FuncionarioId}': {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
