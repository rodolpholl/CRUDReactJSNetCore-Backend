using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListGestores
{
    public class ListGestoresRequestHandler : IRequestHandler<ListGestoresRequest, IEnumerable<ListGestoresResponse>>
    {
        private ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ListGestoresRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<ListGestoresResponse>> Handle(ListGestoresRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return (await _funcionarioRepository.ListGestores(request.LevelCargo))
                    .Select(x => new ListGestoresResponse
                    {
                        GestorId = x.Id,
                        Nome = x.Nome,
                        LevelCargo = x.Cargo.Level
                    }).AsEnumerable();


            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao listar os gestores: {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
