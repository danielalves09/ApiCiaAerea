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
        return Ok(voo);
        //return CreatedAtAction(nameof(AdicionarVoo), new { id = voo.Id }, voo);
    }

    [HttpGet]
    public IActionResult ListarVoos(string? origem, string? destino, DateTime? partida, DateTime? chegada)
    {
        var voos = _vooService.ListarVoos(origem, destino, partida, chegada);
        return Ok(voos);
    }
}