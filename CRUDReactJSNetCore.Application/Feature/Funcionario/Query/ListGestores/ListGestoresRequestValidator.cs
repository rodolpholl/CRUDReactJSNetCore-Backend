using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListGestores
{
    public class ListGestoresRequestValidator : AbstractValidator<ListGestoresRequest>
    {
        public ListGestoresRequestValidator()
        {
            RuleFor(x => x.LevelCargo)
                .GreaterThan(0)
                .LessThanOrEqualTo(6);
        }
    }
}
