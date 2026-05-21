using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Q6.DTOS;

public sealed class CriarOrcamentoRequestDto
{
    [Required(ErrorMessage = "O campo clienteId e obrigatorio.")]
    public int? ClienteId { get; set; }

    [Required(ErrorMessage = "O campo veiculoId e obrigatorio.")]
    public int? VeiculoId { get; set; }

    [Required(ErrorMessage = "Informe pelo menos 1 item no orcamento.")]
    [MinLength(1, ErrorMessage = "O orcamento deve possuir pelo menos 1 item.")]
    public List<CriarOrcamentoItemRequestDto>? Itens { get; set; }
}
