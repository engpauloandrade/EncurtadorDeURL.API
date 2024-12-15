using LinkCutter.Domain.Middleware;

namespace LinkCutter.Domain.Exceptions;

public class TokenInvalidoException : NegocioException
{
    public TokenInvalidoException(string codigo, string mensagem) : base(codigo, mensagem)
    {
    }
}
