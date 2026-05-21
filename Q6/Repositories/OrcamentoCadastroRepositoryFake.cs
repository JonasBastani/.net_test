using System.Threading.Tasks;
using Q6.DTOS;

namespace Q6.Repositories;

public sealed class OrcamentoCadastroRepositoryFake : IOrcamentoCadastroRepository
{
    public Task CadastrarAsync(OrcamentoResponseDto orcamento)
    {
        return Task.CompletedTask;
    }
}
