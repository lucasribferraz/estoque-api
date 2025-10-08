namespace estoque_api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

    }
}
