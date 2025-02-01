using CRUDReactJSNetCore.Application.Helpers;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Auth.Command.EfetuarLogin
{
    public class EfetuarLoginCommandHandler : IRequestHandler<EfetuarLoginCommand, EfetuarLoginResponse>
    {
        private readonly ILogger _logger;

        public EfetuarLoginCommandHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<EfetuarLoginResponse> Handle(EfetuarLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var senhaCorreta = EncryptHelper.Validar(request.Password, request.Funcionario.Senha);
                if (!senhaCorreta)
                    throw new Exception("Acesso Negado. Verifique o email e sua senha informados");

                return new EfetuarLoginResponse
                {
                    Email = request.Funcionario.Email,
                    Name = request.Funcionario.Nome,
                    UserId = request.Funcionario.Id
                };
            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao tentar efetuar login do funcionário: {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
