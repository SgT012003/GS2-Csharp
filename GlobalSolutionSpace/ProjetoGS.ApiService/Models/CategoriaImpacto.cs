namespace ProjetoGS.ApiService.Models;

public class CategoriaImpacto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Saúde, Agricultura, Consumo, etc
    public string Descricao { get; set; } = string.Empty;
    
    public ICollection<Tecnologia> Tecnologias { get; set; } = new List<Tecnologia>();
}
