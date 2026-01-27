namespace CiaAerea.Validators.Aeronave
{
    using System.Data;
    using CiaAerea.Contexts;
    using CiaAerea.ViewModels.Aeronave;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;

    public class ExcluirAeronaveValidator : AbstractValidator<int>
    {
        private readonly CiaAereaContext _context;
        public ExcluirAeronaveValidator(CiaAereaContext context)
        {
            _context = context;

            RuleFor(id => _context.Aeronaves.Include(a => a.Voos).Include(a => a.Manutencoes).FirstOrDefault(a => a.Id == id))
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("A aeronave com o ID informado não existe.")
            .Must(aeronave => aeronave!.Voos.Count == 0).WithMessage("A aeronave não pode ser excluída pois está associada a voos.")
            .Must(aeronave => aeronave!.Manutencoes.Count == 0).WithMessage("A aeronave não pode ser excluída pois está associada a Manutenções.");



        }
    }
}