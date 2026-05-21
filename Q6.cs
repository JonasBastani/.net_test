using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

public sealed class OrcamentoItemResponseDto
{
    public string Descricao { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

[ApiController]
[Route("api/orcamentos")]
public sealed class OrcamentosController : ControllerBase
{
    private readonly OrcamentoService _orcamentoService;

    public OrcamentosController(OrcamentoService orcamentoService)
    {
        _orcamentoService = orcamentoService;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CriarOrcamentoRequestDto request)
    {
        var orcamento = await _orcamentoService.CadastrarAsync(request);
        return Created("api/orcamentos", orcamento);
    }
}

public sealed class OrcamentoService
{
    private readonly IOrcamentoCadastroRepository _orcamentoCadastroRepository;

    public OrcamentoService(IOrcamentoCadastroRepository orcamentoCadastroRepository)
    {
        _orcamentoCadastroRepository = orcamentoCadastroRepository;
    }

    public async Task<OrcamentoResponseDto> CadastrarAsync(CriarOrcamentoRequestDto request)
    {
        var orcamento = OrcamentoResponseDto.FromRequest(request);
        await _orcamentoCadastroRepository.CadastrarAsync(orcamento);

        return orcamento;
    }
}

public interface IOrcamentoCadastroRepository
{
    Task CadastrarAsync(OrcamentoResponseDto orcamento);
}
