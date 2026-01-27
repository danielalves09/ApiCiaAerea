namespace CiaAerea.Entities
{
    public class Piloto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public ICollection<Voo> Voos { get; set; } = null!;

        public Piloto(string nome, string matricula)
        {
            Nome = nome;
            Matricula = matricula;
        }

    }
}
