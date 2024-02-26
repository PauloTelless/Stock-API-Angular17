using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCategoriaProdutoAngular.Models;

[Table("Users")]
public class AuthUser
{
    [Key]
    public Guid UsuarioId { get; set; }
    public string? NomeUsuario { get; set; }
    public string? Senha { get; set; }  

}
