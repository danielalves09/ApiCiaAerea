namespace CiaAerea.Validators.Aeronave
{
    using CiaAerea.Contexts;
    using CiaAerea.ViewModels.Aeronave;
    using FluentValidation;

    public class AtualizarAeronaveValidator : AbstractValidator<AtualizarAeronaveViewModel>
    {

        private readonly CiaAereaContext _context;

        public AtualizarAeronaveValidator(CiaAereaContext context)
        {

            _context = context;

            RuleFor(a => a.Fabricante)
                .NotEmpty().WithMessage("É necessário informar o fabricante da Aeronave")
                .MaximumLength(50).WithMessage("O fabricante deve ter no máximo 50 caracteres.");

            RuleFor(a => a.Modelo)
                .NotEmpty().WithMessage("É necessário informar o Modelo da Aeronave")
                .MaximumLength(50).WithMessage("O modelo deve ter no máximo 50 caracteres.");

            RuleFor(a => a.Codigo)
                .NotEmpty().WithMessage("É necessário informar o Código da Aeronave")
                .MaximumLength(10).WithMessage("O Código deve ter no máximo 10 caracteres.");

            RuleFor(a => a)
                .Must(aeronave => _context.Aeronaves.Count(a => a.Codigo == aeronave.Codigo && a.Id != aeronave.Id) == 0)
                .WithMessage("Já existe uma aeronave cadastrada com esse código.");
        }


    }
}