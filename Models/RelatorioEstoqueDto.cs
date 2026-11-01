namespace CatalogoJogos.Models;

public class RelatorioEstoqueDto
{
    public string Plataforma { get; set; } = string.Empty;
    public int QuantidadeTitulos { get; set; }
    public decimal ValorTotalInventario { get; set; }
}