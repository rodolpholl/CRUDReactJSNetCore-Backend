using CRUDReactJSNetCore.Application.Repository;
using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario
{
    public class ListFuncionarioRequestHandler : IRequestHandler<ListFuncionarioRequest, IEnumerable<ListFuncionarioResponse>>
    {
        private readonly ILogger _logger;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ListFuncionarioRequestHandler(ILogger logger, IFuncionarioRepository funcionarioRepository)
        {
            _logger = logger;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<ListFuncionarioResponse>> Handle(ListFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var listResult = await _funcionarioRepository.ListFuncionarios(request.PageIndex, request.PageCount, request.AddDesativados, request.Filtro);

                return listResult.Select(x => new ListFuncionarioResponse
                {
                    Cargo = x.Cargo.Nome,
                    Nome = x.Nome,
                    Email = x.Email,
                    Gestor = x.Gestor?.Nome,
                    Id = x.Id,
                    Active = x.Active,
                    Telefone = string.Join(", ", x.Telefone),
                    Documento = x.Documento
                }).AsEnumerable();


            }
            catch (Exception ex)
            {
                var msgErr = $"Erro ao listar os funcionários: {Environment.NewLine}{ex.Message}";
                _logger.Error(msgErr, ex);
                throw new Exception(msgErr, ex);
            }
        }
    }
}
