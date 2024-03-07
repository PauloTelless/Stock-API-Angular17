using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCategoriaProdutoAngular.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosAsync()
    {
        try
        {
            var produtos = await _context.Produtos
            .AsNoTracking()
            .ToListAsync();

            return Ok(produtos);
        }
        catch (Exception ex)
        {

            return BadRequest($"Error: {ex.Message}/Produtos sem dados");
        }

    }

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProdutoAsync(Produto produto)
    {

        try
        {
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return Ok(produto);
        }
        catch (Exception ex)
        {

            return BadRequest($"Error: {ex.Message}");
        }

    }

    [HttpPut("{produtoId}")]
    public async Task<ActionResult<Produto>> PutProdutoAsync(Produto produto, string produtoId)
    {
        Guid id;
        try
        {
            if (!Guid.TryParse(produtoId, out id))
            {
                return BadRequest("Não foi possível converter o GUI em ID");
            }

            produto.ProdutoId = id;

            _context.Produtos.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(produto);
        }
        catch (Exception ex)
        {

            return BadRequest($"Error: {ex.Message}");
        }

    }

    [HttpDelete("{produtoId}")]
    public async Task<ActionResult<Produto>> DeleteProdutoAsync(string produtoId)
    {
        try
        {
            var produto = _context.Produtos
           .FirstOrDefault(produto => produto.ProdutoId
           .ToString() == produtoId);

            if (produto == null)
            {
                return NotFound("Id não encontrado");
            }

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return Ok(produto);
        }
        catch (Exception ex)
        {

            return BadRequest($"Error: {ex.Message}");
        }

    }
}
