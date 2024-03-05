using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCategoriaProdutoAngular.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasAsync()
    {
        try
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .ToListAsync();

            return Ok(categorias);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");

        }
        
    }

    [HttpGet("/categoriasProdutos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutoAsync()  
    {
        try
        {
            var categoriasProdutos = await _context.Categorias
            .AsNoTracking()
            .Include(produto => produto.Produtos)
            .ToListAsync();

            return Ok(categoriasProdutos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        
        }
          
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoriaAsync(Categoria categoria)
    {
        try
        {
            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
            
        }
        
    }

    [HttpPut("{categoriaId}")]
    public async Task<ActionResult<Categoria>> PutCategoriaAsync(Categoria categoria, string categoriaId)
    {
        Guid id;
        try
        {
            if (!Guid.TryParse(categoriaId, out id))
            {
                return BadRequest("O ID da categoria é inválido.");
            }

            var categoriaExistente = await _context.Categorias
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.CategoriaId == id);

            if (categoriaExistente == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            categoriaExistente.NomeCategoria = categoria.NomeCategoria;

            foreach (var produto in categoriaExistente.Produtos)
            {
                produto.CategoriaProduto = categoria.NomeCategoria;
            }

            await _context.SaveChangesAsync();

            return Ok(categoriaExistente);
        }
        catch (Exception ex)
        {

            return BadRequest($"Error: {ex.Message}");
        }
        
    }

    [HttpDelete("{categoriaId}")]
    public async Task<ActionResult<Categoria>> DeleteCategoriaAsnc(string categoriaId)
    {
        try
        {
            var categoria = await _context.Categorias
            .Include(p => p.Produtos)
            .FirstOrDefaultAsync(c => c.CategoriaId
            .ToString() == categoriaId);

            if (categoria == null)
            {
                return NotFound($"Categoria: {categoria} não encontrada");
            }
            _context.Produtos.RemoveRange(categoria.Produtos);

            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {

            throw;
        }
        
    }

}
