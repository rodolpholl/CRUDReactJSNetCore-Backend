using CRUDReactJSNetCore.Application.Helpers;
using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Auth.Command.AlterarPassword
{
    public class AlterarPasswordCommandHandler : IRequestHandler<AlterarPasswordCommand, bool>
    {
        private ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public AlterarPasswordCommandHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<bool> Handle(AlterarPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Funcionario.Senha = EncryptHelper.EncriptarPassword(request.NovaSenha);

                await _funcionarioRepository.AlterarFuncionario(request.Funcionario);

                return true;
            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao tentar atualizar os dados de acesso do funcionário: {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
