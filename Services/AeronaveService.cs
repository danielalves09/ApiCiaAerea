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

        public IEnumerable<ListarAeronaveViewModel> ListarAeronaves()
        {
            return _context.Aeronaves.Select(a => new ListarAeronaveViewModel(a.Id, a.Modelo, a.Codigo));

        }

        public DetalhesAeronaveViewModel ListarAeronavePorId(int id)
        {
            var aeronave = _context.Aeronaves.Find(id);
            if (aeronave == null)
            {
                return null;
            }

            return new DetalhesAeronaveViewModel(aeronave.Id, aeronave.Fabricante, aeronave.Modelo, aeronave.Codigo);
        }

        public DetalhesAeronaveViewModel? AtualizarAeronave(AtualizarAeronaveViewModel model)
        {
            var aeronave = _context.Aeronaves.Find(model.Id);
            if (aeronave != null)
            {
                aeronave.Fabricante = model.Fabricante;
                aeronave.Modelo = model.Modelo;
                aeronave.Codigo = model.Codigo;

                _context.Aeronaves.Update(aeronave);
                _context.SaveChanges();

                return new DetalhesAeronaveViewModel(aeronave.Id, aeronave.Fabricante, aeronave.Modelo, aeronave.Codigo);
            }
            return null;

        }

        public void ExcluirAeronave(int id)
        {
            var aeronave = _context.Aeronaves.Find(id);
            if (aeronave != null)
            {
                _context.Aeronaves.Remove(aeronave);
                _context.SaveChanges();
            }

        }
    }
}
