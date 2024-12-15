namespace LinkCutter.Domain.Middleware;

public class ErroTecnicoException : Exception
{
    public string Codigo { get; set; }
    public ErroTecnicoException(string codigo, string mensagem) : base(mensagem)
    {
        this.Codigo = codigo;
    }
}
