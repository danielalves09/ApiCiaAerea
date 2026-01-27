using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.Validators.Piloto;
using CiaAerea.ViewModels.Aeronave;
using CiaAerea.ViewModels.Piloto;
using FluentValidation;

namespace CiaAerea.Services;

public class PilotoService
{
    private readonly CiaAereaContext _context;
    private readonly AdicionarPilotoValidator _adicionarPilotoValidator;

    public PilotoService(CiaAereaContext context, AdicionarPilotoValidator adicionarPilotoValidator)
    {
        _context = context;
        _adicionarPilotoValidator = adicionarPilotoValidator;
    }

    public DetalhesPilotoViewModel AdicionarPiloto(AdicionarPilotoViewModel model)
    {
        _adicionarPilotoValidator.ValidateAndThrow(model);

        var piloto = new Piloto(model.Nome, model.Matricula);

        _context.Pilotos.Add(piloto);
        _context.SaveChanges();

        return new DetalhesPilotoViewModel(piloto.Id, piloto.Nome, piloto.Matricula);

    }

    public IEnumerable<ListarPilotoViewModel> ListarPilotos()
    {
        return _context.Pilotos
            .Select(p => new ListarPilotoViewModel(p.Id, p.Nome));
    }
}