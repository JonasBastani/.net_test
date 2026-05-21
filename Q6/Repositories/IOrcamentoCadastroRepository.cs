using System.Threading.Tasks;
using Q6.DTOS;

namespace Q6.Repositories;

public interface IOrcamentoCadastroRepository
{
    Task CadastrarAsync(OrcamentoResponseDto orcamento);
}
