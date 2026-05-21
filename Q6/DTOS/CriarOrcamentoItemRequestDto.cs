using System.ComponentModel.DataAnnotations;

namespace Q6.DTOS;

public sealed class CriarOrcamentoItemRequestDto
{
    [Required(ErrorMessage = "A descricao do item e obrigatoria.")]
    [MinLength(1, ErrorMessage = "A descricao do item e obrigatoria.")]
    [RegularExpression(@".*\S.*", ErrorMessage = "A descricao do item e obrigatoria.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A quantidade do item e obrigatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade do item deve ser maior que zero.")]
    public int? Quantidade { get; set; }

    [Required(ErrorMessage = "O valor unitario do item e obrigatorio.")]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "O valor unitario do item deve ser maior que zero.")]
    public decimal? ValorUnitario { get; set; }
}
