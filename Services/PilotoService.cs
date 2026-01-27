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
    private readonly AtualizarPilotoValidator _atualizarPilotoValidator;
    private readonly ExcluirPilotoValidator _excluirPilotoValidator;

    public PilotoService(CiaAereaContext context, AdicionarPilotoValidator adicionarPilotoValidator, AtualizarPilotoValidator atualizarPilotoValidator, ExcluirPilotoValidator excluirPilotoValidator)
    {
        _context = context;
        _adicionarPilotoValidator = adicionarPilotoValidator;
        _atualizarPilotoValidator = atualizarPilotoValidator;
        _excluirPilotoValidator = excluirPilotoValidator;
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

    public DetalhesPilotoViewModel? ListarPilotoPorId(int id)
    {
        var piloto = _context.Pilotos.Find(id);

        if (piloto != null)
        {
            return new DetalhesPilotoViewModel(piloto.Id, piloto.Nome, piloto.Matricula);

        }
        return null;


    }

    public DetalhesPilotoViewModel? AtualizarPiloto(AtualizarPilotoViewModel model)
    {
        _atualizarPilotoValidator.ValidateAndThrow(model);

        var piloto = _context.Pilotos.Find(model.Id);

        if (piloto != null)
        {
            piloto.Nome = model.Nome;
            piloto.Matricula = model.Matricula;

            _context.Pilotos.Update(piloto);
            _context.SaveChanges();

            return new DetalhesPilotoViewModel(piloto.Id, piloto.Nome, piloto.Matricula);
        }

        return null;
    }

    public void ExcluirPiloto(int id)
    {
        var piloto = _context.Pilotos.Find(id);

        if (piloto != null)
        {
            _context.Pilotos.Remove(piloto);
            _context.SaveChanges();

        }
    }
}