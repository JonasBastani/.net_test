using System.Threading.Tasks;
using Q6.DTOS;
using Q6.Repositories;

namespace Q6.Services;

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
