using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.ViewModels.Aeronave;

namespace CiaAerea.Services
{
    public class AeronaveService
    {
        private readonly CiaAereaContext _context;

        public AeronaveService(CiaAereaContext context)
        {
            _context = context;
        }

        public DetalhesAeronaveViewModel AdicionarAeronave(AdicionarAeronaveViewModel model)
        {
            var aeronave = new Aeronave(model.Fabricante, model.Modelo, model.Codigo);

            _context.Aeronaves.Add(aeronave);
            _context.SaveChanges();

            return new DetalhesAeronaveViewModel(aeronave.Id, aeronave.Fabricante, aeronave.Modelo, aeronave.Codigo);
        }
    }
}
