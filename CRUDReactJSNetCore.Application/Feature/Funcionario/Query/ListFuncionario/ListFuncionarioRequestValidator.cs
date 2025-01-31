using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario
{
    public class ListFuncionarioRequestValidator : AbstractValidator<ListFuncionarioRequest>
    {
        public ListFuncionarioRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0);

            RuleFor(x => x.PageCount)
                .GreaterThan(0);
        }
    }
}
