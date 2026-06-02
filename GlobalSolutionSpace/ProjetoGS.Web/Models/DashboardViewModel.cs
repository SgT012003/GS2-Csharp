namespace ProjetoGS.Web.Models;

public class DashboardViewModel
{
    public int TotalTecnologias { get; set; }
    public IEnumerable<SetorStats> PorSetor { get; set; } = new List<SetorStats>();
    public IEnumerable<TecnologiaResumo> UltimasCadastradas { get; set; } = new List<TecnologiaResumo>();
}

public class SetorStats
{
    public string Setor { get; set; } = string.Empty;
    public int Quantidade { get; set; }
}

public class TecnologiaResumo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string OrigemEspacial { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}
