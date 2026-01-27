namespace CiaAerea.Validators.Aeronave
{
    using CiaAerea.Contexts;
    using CiaAerea.ViewModels.Aeronave;
    using FluentValidation;

    public class AdicionarAeronaveValidator : AbstractValidator<AdicionarAeronaveViewModel>
    {

        private readonly CiaAereaContext _context;

        public AdicionarAeronaveValidator(CiaAereaContext context)
        {
            _context = context;
        }

        public AdicionarAeronaveValidator()
        {
            RuleFor(a => a.Fabricante)
                .NotEmpty().WithMessage("É necessário informar o fabricante da Aeronave")
                .MaximumLength(50).WithMessage("O fabricante deve ter no máximo 50 caracteres.");

            RuleFor(a => a.Modelo)
                .NotEmpty().WithMessage("É necessário informar o Modelo da Aeronave")
                .MaximumLength(50).WithMessage("O modelo deve ter no máximo 50 caracteres.");

            RuleFor(a => a.Codigo)
                .NotEmpty().WithMessage("É necessário informar o Código da Aeronave")
                .MaximumLength(10).WithMessage("O Código deve ter no máximo 10 caracteres.")
                .Must(codigo => !_context.Aeronaves.Any(a => a.Codigo == codigo))
                .WithMessage("Já existe uma aeronave cadastrada com esse código.");

        }


    }
}