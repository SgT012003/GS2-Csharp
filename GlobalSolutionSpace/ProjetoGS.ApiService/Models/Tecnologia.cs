namespace ProjetoGS.ApiService.Models;

public class Tecnologia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string OrigemEspacial { get; set; } = string.Empty; // Missão ou contexto de origem
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public int CategoriaImpactoId { get; set; }
    public CategoriaImpacto? CategoriaImpacto { get; set; }
}
