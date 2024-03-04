using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCategoriaProdutoAngular.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public Guid ProdutoId { get; set; }

    public string? NomeProduto { get; set; }

    public string? MarcaProduto { get; set; }

    public string? CategoriaProduto { get; set; }

    public string? DescricaoProduto { get; set; }

    public string? PrecoProduto { get; set; }

    public int QuantidadeProduto { get; set; }

    public Guid CategoriaId { get; set; }
}
