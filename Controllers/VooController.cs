using CiaAerea.Services;
using CiaAerea.ViewModels.Voos;
using Microsoft.AspNetCore.Mvc;

namespace CiaAerea.Controllers;

[Route("api/voos")]
[ApiController]
public class VooController : ControllerBase
{
    private readonly VooService _vooService;

    public VooController(Services.VooService vooService)
    {
        _vooService = vooService;
    }

    [HttpPost]
    public IActionResult AdicionarVoo(AdicionarVooViewModel model)
    {
        var voo = _vooService.AdicionarVoo(model);

        return CreatedAtAction(nameof(ListarVooPeloId), new { id = voo.Id }, voo);
    }

    [HttpGet]
    public IActionResult ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
    {
        var voos = _vooService.ListarVoos(origem, destino, partida, chegada);
        return Ok(voos);
    }

    [HttpGet("{id}")]
    public IActionResult ListarVooPeloId(int id)
    {
        var voo = _vooService.ListarVooPeloId(id);
        if (voo == null)
        {
            return NotFound();
        }
        return Ok(voo);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarVoo(int id, AtualizarVooViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest("O ID do voo na URL não corresponde ao ID no corpo da requisição.");
        }

        var vooAtualizado = _vooService.AtualizarVoo(model);
        if (vooAtualizado == null)
        {
            return NotFound();
        }
        return Ok(vooAtualizado);
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluirVoo(int id)
    {
        _vooService.ExcluirVoo(id);
        return NoContent();
    }
}