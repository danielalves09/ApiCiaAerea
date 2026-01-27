namespace CiaAerea.ViewModels.Aeronave
{
    public class DetalhesAeronaveViewModel
    {

        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Fabricante { get; set; }
        public string Codigo { get; set; }


        public DetalhesAeronaveViewModel(int id, string modelo, string fabricante, string codigo)
        {
            Id = id;
            Modelo = modelo;
            Fabricante = fabricante;
            Codigo = codigo;
        }

    }
}
