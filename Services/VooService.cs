using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.Validators.Voo;
using CiaAerea.ViewModels.Voos;
using FluentValidation;

namespace CiaAerea.Services;

public class VooService
{
    private readonly CiaAereaContext _context;
    private readonly AdicionarVooValidator _adicionarVooValidator;

    public VooService(CiaAereaContext context, AdicionarVooValidator adicionarVooValidator)
    {
        _context = context;
        _adicionarVooValidator = adicionarVooValidator;
    }

    public DetalhesVooViewModel AdicionarVoo(AdicionarVooViewModel model)
    {
        _adicionarVooValidator.ValidateAndThrow(model);

        // Lógica para adicionar o voo ao banco de dados
        // Exemplo simplificado:
        var voo = new Voo
        (
            model.Origem,
            model.Destino,
            model.DataHoraPartida,
            model.DataHoraChegada,
            model.AeronaveId,
            model.PilotoId

        );

        _context.Voos.Add(voo);
        _context.SaveChanges();

        return new DetalhesVooViewModel
        (
            voo.Id,
            voo.Origem,
            voo.Destino,
            voo.DataHoraPartida,
            voo.DataHoraChegada,
            voo.AeronaveId,
            voo.PilotoId
            );
    }

    public IEnumerable<ListarVooViewModel> ListarVoos()
    {
        return _context.Voos.Select(voo => new ListarVooViewModel(
            voo.Id,
            voo.Origem,
            voo.Destino,
            voo.DataHoraPartida,
            voo.DataHoraChegada
        ));


    }

}