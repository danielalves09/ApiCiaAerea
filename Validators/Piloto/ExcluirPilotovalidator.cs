namespace CiaAerea.Validators.Piloto
{
    using CiaAerea.Contexts;
    using CiaAerea.ViewModels.Piloto;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;

    public class ExcluirPilotoValidator : AbstractValidator<int>
    {

        private readonly CiaAereaContext _context;

        public ExcluirPilotoValidator(CiaAereaContext context)
        {

            _context = context;

            RuleFor(id => _context.Pilotos.Include(p => p.Voos).FirstOrDefault(p => p.Id == id))
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Piloto não encontrado.")
                .Must(piloto => piloto.Voos.Count == 0)
                .WithMessage("Não é possível excluir o piloto pois ele está associado a voos.");

        }


    }
}