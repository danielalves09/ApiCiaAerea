namespace CiaAerea.ViewModels.Aeronave
{
    public class ListarAeronaveViewModel
    {

        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Codigo { get; set; }


        public ListarAeronaveViewModel(int id, string modelo, string codigo)
        {
            Id = id;
            Modelo = modelo;
            Codigo = codigo;
        }


    }
}
