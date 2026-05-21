using System.Collections.Generic;
using System.Linq;

namespace Q6.DTOS;

public sealed class OrcamentoResponseDto
{
    public int ClienteId { get; set; }
    public int VeiculoId { get; set; }
    public List<OrcamentoItemResponseDto> Itens { get; set; } = new List<OrcamentoItemResponseDto>();
    public decimal Total { get; set; }

    public static OrcamentoResponseDto FromRequest(CriarOrcamentoRequestDto request)
    {
        var itens = request.Itens!
            .Select(item => new OrcamentoItemResponseDto
            {
                Descricao = item.Descricao!,
                Quantidade = item.Quantidade!.Value,
                ValorUnitario = item.ValorUnitario!.Value,
                Subtotal = item.Quantidade.Value * item.ValorUnitario.Value
            })
            .ToList();

        return new OrcamentoResponseDto
        {
            ClienteId = request.ClienteId!.Value,
            VeiculoId = request.VeiculoId!.Value,
            Itens = itens,
            Total = itens.Sum(item => item.Subtotal)
        };
    }
}
