namespace ProjetoGS.ApiService.Models;

public class Tecnologia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public int OrigemId { get; set; }
    public Origem? Origem { get; set; }

    public int CategoriaImpactoId { get; set; }
    public CategoriaImpacto? CategoriaImpacto { get; set; }
}
