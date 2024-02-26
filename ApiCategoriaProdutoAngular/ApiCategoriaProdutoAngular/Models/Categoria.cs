using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCategoriaProdutoAngular.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();   
    }

    [Key]
    public Guid CategoriaId { get; set; }

    public string? NomeCategoria { get; set; }

    public Collection<Produto> Produtos { get; set; }
}
