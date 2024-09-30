namespace ToAToa.Domain.Abstractions;

public abstract class Entidade
{
    protected Entidade(int id)
    {
        Id = id;
        Ativo = true;
    }

    protected Entidade()
    {
    }

    public int Id { get; protected init; }
    public bool Ativo { get; protected set; }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}
