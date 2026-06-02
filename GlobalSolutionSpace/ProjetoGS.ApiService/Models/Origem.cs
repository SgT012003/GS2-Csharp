namespace ProjetoGS.ApiService.Models;

public class Origem
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    public ICollection<Tecnologia> Tecnologias { get; set; } = new List<Tecnologia>();
}
