using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiCategoriaProdutoAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersCreatedController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersCreatedController(AppDbContext context)
    {
        _context = context; 
    }

    [HttpPost]
    public async Task<ActionResult<CreateUser>> PostUserAsync(CreateUser user)
    {
        var authUser = new AuthUser();

        authUser.UsuarioId = user.UserCreatedId;
        authUser.NomeUsuario = user.NomeUsuario;
        authUser.Senha = user.Senha;

        _context.Users.Add(authUser);

        await _context.SaveChangesAsync();

        return Ok(user);
    }
}
