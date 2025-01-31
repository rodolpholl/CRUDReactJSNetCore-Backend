using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.GetFuncionarioById
{
    public class GetFuncionarioByIdRequestValidator : AbstractValidator<GetFuncionarioByIdRequest>
    {
        public GetFuncionarioByIdRequestValidator()
        {
            RuleFor(x => x.FuncionarioId)
                .GreaterThan(0);
        }
    }
}
