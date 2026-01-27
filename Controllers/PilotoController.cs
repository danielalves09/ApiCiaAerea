using CiaAerea.Services;
using CiaAerea.ViewModels.Piloto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;

namespace CiaAerea.Controllers;

[Route("api/pilotos")]
[ApiController]
public class PilotoController : ControllerBase
{
    private readonly PilotoService _pilotoService;

    public PilotoController(Services.PilotoService pilotoService)
    {
        _pilotoService = pilotoService;
    }

    [HttpPost]
    public IActionResult AdicionarPiloto(AdicionarPilotoViewModel model)
    {
        var piloto = _pilotoService.AdicionarPiloto(model);
        return Ok(piloto);
        //return CreatedAtAction(nameof(AdicionarPiloto), new { id = piloto.Id }, piloto);
    }

    [HttpGet]
    public IActionResult ListarPilotos()
    {
        var pilotos = _pilotoService.ListarPilotos();
        return Ok(pilotos);
    }

    [HttpGet("{id}")]
    public IActionResult ListarPilotoPorId(int id)
    {
        var piloto = _pilotoService.ListarPilotoPorId(id);
        if (piloto != null)
        {
            return Ok(piloto);

        }
        return NotFound();
    }

}