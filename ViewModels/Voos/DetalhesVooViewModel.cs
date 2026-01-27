using CiaAerea.ViewModels.Aeronave;
using CiaAerea.ViewModels.Piloto;

namespace CiaAerea.ViewModels.Voos;

public class DetalhesVooViewModel
{


    public int Id { get; set; }
    public string Origem { get; set; }
    public string Destino { get; set; }
    public DateTime DataHoraPartida { get; set; }
    public DateTime DataHoraChegada { get; set; }
    public int AeronaveId { get; set; }
    public int PilotoId { get; set; }

    public DetalhesAeronaveViewModel? Aeronave { get; set; }
    public DetalhesPilotoViewModel? Piloto { get; set; }

}