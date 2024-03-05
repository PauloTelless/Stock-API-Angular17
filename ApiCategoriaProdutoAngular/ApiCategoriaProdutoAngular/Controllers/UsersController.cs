using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Models;
using ApiCategoriaProdutoAngular.Services;
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
    public async Task<ActionResult<IEnumerable<AuthUser>>> GetUsersAsync(TokenService tokenService)
    {
        try
        {
            var token  = tokenService.GenereteToken(null);
            Console.WriteLine(tokenService);
            var users = await _context.Users
            .AsNoTracking()
            .ToListAsync();

            return Ok(users);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);  
        }
           
    }

    [HttpGet("auth/{usuarioId}")]
    public async Task<ActionResult<AuthUser>> GetUserAsync(string usuarioId) 
    {
        try
        {
        var user = await _context.Users
                .FirstOrDefaultAsync(userId => userId.UsuarioId.
                ToString() == usuarioId);   

        return Ok(user);    

        }
        catch (Exception ex)
        {

            return BadRequest($"Error: {ex.Message}");
        }
    }
}
