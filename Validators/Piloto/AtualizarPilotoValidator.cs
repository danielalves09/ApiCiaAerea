namespace CiaAerea.Validators.Piloto
{
    using CiaAerea.Contexts;
    using CiaAerea.ViewModels.Piloto;
    using FluentValidation;

    public class AtualizarPilotoValidator : AbstractValidator<AtualizarPilotoViewModel>
    {

        private readonly CiaAereaContext _context;

        public AtualizarPilotoValidator(CiaAereaContext context)
        {

            _context = context;

            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("É necessário informar o nome do piloto")
                .MaximumLength(100).WithMessage("O nome do Piloto deve ter no máximo 100 caracteres.");

            RuleFor(a => a.Matricula)
                .NotEmpty().WithMessage("É necessário informar a Matricula do piloto")
                .MaximumLength(10).WithMessage("O Código deve ter no máximo 10 caracteres.");

            RuleFor(p => p)
            .Must(piloto => _context.Pilotos.Count(p => p.Matricula == piloto.Matricula && p.Id != piloto.Id) == 0)
            .WithMessage("Já existe um piloto cadastrado com essa matrícula.");

        }


    }
}