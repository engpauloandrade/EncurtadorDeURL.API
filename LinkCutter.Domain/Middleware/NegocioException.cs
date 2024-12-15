namespace LinkCutter.Domain.Middleware
{
    public class NegocioException : Exception
    {
        public string Codigo { get; set; }
        public NegocioException(string codigo, string mensagem) : base(mensagem)
        {
            this.Codigo = codigo;
        }
    }
}
