namespace ToAToa.Domain.Entities;

public class Atividade(
    int id,
    string descricao)
{
    public int Id { get; private set; } = id;
    public string Descricao { get; private set; } = descricao;
}
