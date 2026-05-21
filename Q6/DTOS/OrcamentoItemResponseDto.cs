namespace Q6.DTOS;

public sealed class OrcamentoItemResponseDto
{
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
