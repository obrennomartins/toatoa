using ToAToa.Domain.Abstractions;

namespace ToAToa.Domain.Entities;

public class Atividade(
    int id,
    string descricao) : Entidade(id)
{
    public string Descricao { get; private set; } = descricao;
}
