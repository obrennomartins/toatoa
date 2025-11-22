using Microsoft.EntityFrameworkCore;
using ToAToa.Domain.Entities;
using ToAToa.Domain.Interfaces;

namespace ToAToa.DataAccess.Repositories;

public class AtividadeRepository(ToAToaDbContext dbContext) : IAtividadeRepository
{
    public async Task<Atividade?> ObterAtividadeAleatoriaAsync()
    {
        var numItens = await dbContext.Atividades.CountAsync();
        var random = new Random();
        var numAleatorio = random.Next(numItens);

        var atividadeAleatoria = await dbContext.Atividades.Skip(numAleatorio).FirstOrDefaultAsync();

        return atividadeAleatoria;
    }

    public async Task<int> ObterTotalAtividadesAsync() =>
        await dbContext.Atividades.CountAsync();

    public async Task<Atividade?> ObterAtividadePorSkipAsync(int skip) =>
        await dbContext.Atividades.OrderBy(a => a.Id).Skip(skip).FirstOrDefaultAsync();
}
