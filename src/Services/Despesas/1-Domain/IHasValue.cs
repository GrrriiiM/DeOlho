namespace DeOlho.Services.Despesas
{
    public interface IHasValor
    {
        decimal Valor { get; }
    }

    public interface IHasCPF
    {
        long CPF { get; }
    }

    public interface IHasTipoId
    {
        int TipoId { get; }
    }

    public interface IHasAno
    {
        int Ano { get; }
    }

    public interface IHasMes
    {
        int Mes { get; }
    }
}