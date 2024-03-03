using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCategoriaProdutoAngular.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context; 
    }

    [HttpGet]
    public ActionResult<IEnumerable<AuthUser>> GetUsers()
    {
        var users = _context.Users.AsNoTracking().ToList();

        return Ok(users);   
    }

    [HttpGet("auth/{usuarioId}")]

    public ActionResult<AuthUser> GetUser(string usuarioId) 
    {
        var user = _context.Users.FirstOrDefault(userId => userId.UsuarioId.ToString() == usuarioId);   

        return Ok(user);    
    }
}
