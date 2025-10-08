namespace estoque_api.Models.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao {  get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;

    }
}
