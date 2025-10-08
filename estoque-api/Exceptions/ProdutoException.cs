namespace estoque_api.Exceptions
{
    public class ProdutoException : Exception
    {
        public ProdutoException(string erroMessage)
            : base (erroMessage)
        {
            
        }
    }
}
