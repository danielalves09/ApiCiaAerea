using CiaAerea.Contexts;
using CiaAerea.ViewModels.Voos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CiaAerea.Validators.Voo;

public class AdicionarVooValidator : AbstractValidator<AdicionarVooViewModel>
{
    private readonly CiaAereaContext _context;

    public AdicionarVooValidator(CiaAereaContext context)
    {
        _context = context;

        RuleFor(v => v.Origem)
            .NotEmpty().WithMessage("É necessário informar a origem do voo.")
            .Length(3).WithMessage("Aeroporto de Origem inválido");

        RuleFor(v => v.Destino)
            .NotEmpty().WithMessage("É necessário informar o destino do voo.")
            .Length(3).WithMessage("Aeroporto de Destino inválido");

        RuleFor(v => v)
            .Must(v => v.DataHoraPartida > DateTime.Now)
            .WithMessage("A data/hora devem ser maiores que a data/hora atual. ")
            .Must(v => v.DataHoraChegada > v.DataHoraPartida)
            .WithMessage("A data/hora de chegada deve ser maior que a data/hora de partida.");

        RuleFor(v => v).Custom((voo, ValidationContext) =>
        {
            var piloto = _context.Pilotos.Include(p => p.Voos).FirstOrDefault(p => p.Id == voo.PilotoId);
            if (piloto == null)
            {
                ValidationContext.AddFailure("Piloto invalido");
            }
            else
            {
                var pilotoEmVoo = piloto.Voos.Any(v =>
                    (v.DataHoraPartida <= voo.DataHoraPartida) && (v.DataHoraChegada >= voo.DataHoraChegada)
                || (v.DataHoraChegada >= voo.DataHoraPartida) && (v.DataHoraChegada <= voo.DataHoraChegada)
                || (v.DataHoraChegada >= voo.DataHoraPartida) && (v.DataHoraChegada <= voo.DataHoraChegada));

                if (pilotoEmVoo)
                {
                    ValidationContext.AddFailure("este piloto estará em voo no horario selecionado");
                }
            }

            var aeronave = _context.Aeronaves.Include(a => a.Voos).Include(a => a.Manutencoes).FirstOrDefault(a => a.Id == voo.AeronaveId);

            if (aeronave == null)
            {
                ValidationContext.AddFailure("Aeronave invalida");
            }
            else
            {

                var aeronaveEmVoo = aeronave.Voos.Any(v =>
                    (v.DataHoraPartida <= voo.DataHoraPartida) && (v.DataHoraChegada >= voo.DataHoraChegada)
                || (v.DataHoraChegada >= voo.DataHoraPartida) && (v.DataHoraChegada <= voo.DataHoraChegada)
                || (v.DataHoraChegada >= voo.DataHoraPartida) && (v.DataHoraChegada <= voo.DataHoraChegada));

                if (aeronaveEmVoo)
                {
                    ValidationContext.AddFailure("esta aeronave estará em voo no horario selecionado");
                }

                var aeronaveEmManutencao = aeronave.Manutencoes.Any(m =>
                    (m.DataHora >= voo.DataHoraPartida) && (m.DataHora <= voo.DataHoraChegada));


                if (aeronaveEmManutencao)
                {
                    ValidationContext.AddFailure("esta aeronave estará em manutenção no horario selecionado");
                }
            }
        });

    }
}