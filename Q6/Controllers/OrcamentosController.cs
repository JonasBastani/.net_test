using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Q6.DTOS;
using Q6.Services;

namespace Q6.Controllers;

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
