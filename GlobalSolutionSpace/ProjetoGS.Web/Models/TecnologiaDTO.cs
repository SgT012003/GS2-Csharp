namespace ProjetoGS.Web.Models;

public class TecnologiaDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }

    public int OrigemId { get; set; }
    public OrigemDTO? Origem { get; set; }

    public int CategoriaImpactoId { get; set; }
    public CategoriaImpactoDTO? CategoriaImpacto { get; set; }
}
