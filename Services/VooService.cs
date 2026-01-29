using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.Validators.Voo;
using CiaAerea.ViewModels.Aeronave;
using CiaAerea.ViewModels.Piloto;
using CiaAerea.ViewModels.Voos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CiaAerea.Services;

public class VooService
{
    private readonly CiaAereaContext _context;
    private readonly AdicionarVooValidator _adicionarVooValidator;
    private readonly AtualizarVooValidator _atualizarVooValidator;
    private readonly ExcluirVooValidator _excluirVooValidator;

    public VooService(CiaAereaContext context, AdicionarVooValidator adicionarVooValidator, AtualizarVooValidator atualizarVooValidator, ExcluirVooValidator excluirVooValidator)
    {
        _context = context;
        _adicionarVooValidator = adicionarVooValidator;
        _atualizarVooValidator = atualizarVooValidator;
        _excluirVooValidator = excluirVooValidator;
    }

    public DetalhesVooViewModel AdicionarVoo(AdicionarVooViewModel model)
    {
        _adicionarVooValidator.ValidateAndThrow(model);


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

        return ListarVooPeloId(voo.Id)!;
    }

    public IEnumerable<ListarVooViewModel> ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
    {

        var filtroOrigem = (Voo voo) => string.IsNullOrWhiteSpace(origem) || voo.Origem == origem;
        var filtroDestino = (Voo voo) => string.IsNullOrWhiteSpace(destino) || voo.Destino == destino;
        var filtroPartida = (Voo voo) => !partida.HasValue || voo.DataHoraPartida == partida;
        var filtroChegada = (Voo voo) => !chegada.HasValue || voo.DataHoraChegada == chegada;

        return _context.Voos
                .Where(filtroOrigem)
                .Where(filtroDestino)
                .Where(filtroPartida)
                .Where(filtroChegada)
                .Select(voo => new ListarVooViewModel(
            voo.Id,
            voo.Origem,
            voo.Destino,
            voo.DataHoraPartida,
            voo.DataHoraChegada
        ));


    }

    public DetalhesVooViewModel? ListarVooPeloId(int id)
    {
        var voo = _context.Voos.Include(v => v.Aeronave)
                                .Include(v => v.Piloto)
                                .FirstOrDefault(v => v.Id == id);
        if (voo != null)
        {
            var resultado = new DetalhesVooViewModel(
           voo.Id,
           voo.Origem,
           voo.Destino,
           voo.DataHoraPartida,
           voo.DataHoraChegada,
           voo.AeronaveId,
           voo.PilotoId
       );

            resultado.Aeronave = new DetalhesAeronaveViewModel
            (
                voo.Aeronave.Id,
                voo.Aeronave.Fabricante,
                voo.Aeronave.Modelo,
                voo.Aeronave.Codigo
            );

            resultado.Piloto = new DetalhesPilotoViewModel
            (
                voo.Piloto.Id,
                voo.Piloto.Nome,
                voo.Piloto.Matricula
            );
            return resultado;
        }


        return null;


    }

    public DetalhesVooViewModel? AtualizarVoo(AtualizarVooViewModel model)
    {

        _atualizarVooValidator.ValidateAndThrow(model);

        var voo = _context.Voos.FirstOrDefault(v => v.Id == model.Id);
        if (voo != null)
        {
            voo.Origem = model.Origem;
            voo.Destino = model.Destino;
            voo.DataHoraPartida = model.DataHoraPartida;
            voo.DataHoraChegada = model.DataHoraChegada;
            voo.AeronaveId = model.AeronaveId;
            voo.PilotoId = model.PilotoId;

            _context.Voos.Update(voo);
            _context.SaveChanges();

            return ListarVooPeloId(voo.Id);
        }


        return null;

    }

    public void ExcluirVoo(int id)
    {

        _excluirVooValidator.ValidateAndThrow(id);
        var voo = _context.Voos.Find(id);
        if (voo != null)
        {
            _context.Voos.Remove(voo);
            _context.SaveChanges();
        }
    }


}