namespace CiaAerea.ViewModels.Aeronave
{
    public class AdicionarAeronaveViewModel
    {


        public string Modelo { get; set; }
        public string Fabricante { get; set; }
        public string Codigo { get; set; }

        public AdicionarAeronaveViewModel(string modelo, string fabricante, string codigo)
        {
            Modelo = modelo;
            Fabricante = fabricante;
            Codigo = codigo;
        }
    }
}
