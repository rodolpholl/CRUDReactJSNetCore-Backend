using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListCargos
{
    public class ListCargosRequestHandler : IRequestHandler<ListCargosRequest, IEnumerable<ListCargosResponse>>
    {
        private ILogger _logger;
        private readonly ICargoRepository _cargoRepository;

        public ListCargosRequestHandler(ILogger logger, ICargoRepository cargoRepository)
        {
            _logger = logger;
            _cargoRepository = cargoRepository;
        }

        public async Task<IEnumerable<ListCargosResponse>> Handle(ListCargosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return (await _cargoRepository.ListCargos()).Select(x => new ListCargosResponse
                {
                    Id = x.Id,
                    Name = x.Nome,
                    Level = x.Level
                }).AsEnumerable();
            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao listar os cargos: {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
