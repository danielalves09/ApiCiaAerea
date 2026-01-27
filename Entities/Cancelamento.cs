namespace CiaAerea.Entities
{
    public class Cancelamento
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public DateTime DataHoraNotificacao { get; set; }

        public int VooId { get; set; }
        public Voo Voo { get; set; } = null!;


        public Cancelamento(string motivo, DateTime dataHoraNotificacao, int vooId)
        {
            Motivo = motivo;
            DataHoraNotificacao = dataHoraNotificacao;
            VooId = vooId;
        }
    }
}
