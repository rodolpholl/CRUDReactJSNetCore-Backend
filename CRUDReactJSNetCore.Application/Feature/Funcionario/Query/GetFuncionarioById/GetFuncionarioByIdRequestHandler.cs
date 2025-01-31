using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.GetFuncionarioById
{
    public class GetFuncionarioByIdRequestHandler : IRequestHandler<GetFuncionarioByIdRequest, GetFuncionarioByIdResponse>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public GetFuncionarioByIdRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<GetFuncionarioByIdResponse> Handle(GetFuncionarioByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _funcionarioRepository.GetFuncionarioById(request.FuncionarioId, false);

                return new GetFuncionarioByIdResponse()
                {
                    CargoId = result.CargoId,
                    DataNascimento = result.DataNascimento,
                    Email = result.Email,
                    Documento = result.Documento,
                    GestorId = result.GestorId,
                    Nome = result.Nome,
                    Telefone = result.Telefone,
                    Id = result.Id
                };
            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao buscar informações do funcionário '{request.FuncionarioId}': {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
