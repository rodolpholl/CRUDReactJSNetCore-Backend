using CRUDReactJSNetCore.Application.Models;
using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using FuncionarioDomain = CRUDReactJSNetCore.Domain.Entities.Funcionario;

namespace CRUDReactJSNetCore.Application.Validators
{
    public abstract class FuncionarioValidatorBase<T> : AbstractValidator<T> where T : FuncionarioModel
    {
        protected readonly IFuncionarioRepository _funcionarioRepository;
        protected readonly ICargoRepository _cargoRepository;

        protected FuncionarioDomain _gestorEnviado;
        protected Cargo _cargoEnviado;

        public FuncionarioValidatorBase(IFuncionarioRepository funcionarioRepository, ICargoRepository cargoRepository)
        {
            _funcionarioRepository = funcionarioRepository;
            _cargoRepository = cargoRepository;


            RuleFor(x => x.DataNascimento).Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(x =>
                    x.Date < DateTime.Now.AddYears(-18).Date
                    )
                .WithMessage("Funcionario cadastrado precisa ser maior de 18 anos.");

            RuleFor(x => x.Gestor).Cascade(CascadeMode.Stop)
                .NotNull()
                .MustAsync(async (model, x, ct) =>
                {
                    if (x == null)
                        model.Gestor = await _funcionarioRepository.GetFuncionarioById(model.GestorId);

                    return model.Cargo.Level > model.Gestor.Cargo.Level;
                })
                .WithMessage("O nível do Cargo cadastrado deve ser inferior ao do gestor");

            RuleFor(x => x.Cargo).Cascade(CascadeMode.Stop)
                .NotNull();

            RuleFor(x => x.Nome).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(80);

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Telefone).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Must(x => x[0].Length > 0)
                .WithMessage("É preciso informar ao menos um telefone válido");

            RuleFor(x => x.Documento)
                .NotEmpty();
        }

        protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
        {


            if (context.InstanceToValidate.CargoId <= 0)
            {
                result.Errors.Add(new ValidationFailure
                {
                    ErrorMessage = $"Informe o Cargo",
                    Severity = Severity.Error
                });
            }
            else
            {

                _cargoEnviado = Task.Run(() => _cargoRepository.GetCargoById(context.InstanceToValidate.CargoId))
                                    .GetAwaiter().GetResult();

                if (_cargoEnviado == null)
                {
                    result.Errors.Add(new ValidationFailure
                    {
                        ErrorMessage = $"Nenhum cargo encontrado com o Id '{context.InstanceToValidate.CargoId}'",
                        Severity = Severity.Error
                    });

                }
                else
                    context.InstanceToValidate.Cargo = _cargoEnviado;
            }

            if (context.InstanceToValidate.GestorId <= 0)
            {
                result.Errors.Add(new ValidationFailure
                {
                    ErrorMessage = $"Informe o Gestor",
                    Severity = Severity.Error
                });
            }
            else
            {

                _gestorEnviado = Task.Run(() => _funcionarioRepository.GetFuncionarioById(context.InstanceToValidate.GestorId))
                                    .GetAwaiter().GetResult();

                if (_gestorEnviado == null)
                {
                    result.Errors.Add(new ValidationFailure
                    {
                        ErrorMessage = $"Nenhum Gestor encontrado com o Id '{context.InstanceToValidate.GestorId}'",
                        Severity = Severity.Error
                    });

                }
                else
                    context.InstanceToValidate.Gestor = _gestorEnviado;
            }


            return base.PreValidate(context, result);
        }
    }
}
