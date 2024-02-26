using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCategoriaProdutoAngular.Models;

[Table("UsersCreated")]
public class CreateUser
{
    [Key]
    public Guid UserCreatedId { get; set; }     

    public string? NomeUsuario { get; set; }

    public string? EmailUsuario { get; set; }

    public string? Senha { get; set; }  

    public string? ConfirmarSenha { get; set; }     
}
