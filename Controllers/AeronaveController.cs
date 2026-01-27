using CiaAerea.Services;
using CiaAerea.ViewModels.Aeronave;
using Microsoft.AspNetCore.Mvc;

namespace CiaAerea.Controllers
{
    [Route("api/aeronaves")]
    public class AeronaveController : ControllerBase
    {
        private readonly AeronaveService _aeronaveService;

        public AeronaveController(AeronaveService aeronaveService)
        {
            _aeronaveService = aeronaveService;
        }

        [HttpPost]
        public IActionResult AdicionarAeronave([FromBody] AdicionarAeronaveViewModel model)
        {
            var aeronave = _aeronaveService.AdicionarAeronave(model);
            return Ok(aeronave);
        }

        [HttpGet]
        public IActionResult ListarAeronaves()
        {
            var aeronaves = _aeronaveService.ListarAeronaves();
            return Ok(aeronaves);
        }

        [HttpGet("{id}")]
        public IActionResult ListarAeronavePorId(int id)
        {
            var aeronave = _aeronaveService.ListarAeronavePorId(id);
            if (aeronave == null)
            {
                return NotFound();
            }
            return Ok(aeronave);
        }
    }
}
