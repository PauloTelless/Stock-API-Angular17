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
    public ActionResult<IEnumerable<Categoria>> GetCategorias()
    {
        var categorias = _context.Categorias.AsNoTracking().ToList();

        return categorias;
    }

    [HttpGet("/categoriasProdutos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProduto()  
    {
        var categoriasProdutos = _context.Categorias
            .AsNoTracking()
            .Include(produto => produto.Produtos)
            .ToList();

        return categoriasProdutos;  
    }

    [HttpPost]
    public ActionResult<Categoria> PostCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);

        _context.SaveChanges(); 

        return Ok(categoria);
    }

    [HttpPut("{categoriaId}")]
    public ActionResult<Categoria> PutCategoria(Categoria categoria, string categoriaId)
    {
        Guid id;
        if (!Guid.TryParse(categoriaId, out id))
        {
            return BadRequest("O ID da categoria é inválido.");
        }

        categoria.CategoriaId = id;

        _context.Categorias.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();

        return categoria;
    }


    [HttpDelete("{categoriaId}")]
    public ActionResult<Categoria> DeleteCategoria(string categoriaId)
    {
        var categoria = _context.Categorias
            .Include(p => p.Produtos)
            .FirstOrDefault(c => c.CategoriaId
            .ToString() == categoriaId);

        if (categoria == null)
        {
            return NotFound();
        }
        _context.Produtos.RemoveRange(categoria.Produtos);

        _context.Categorias.Remove(categoria);

        _context.SaveChanges();

        return Ok(categoria);
    }

}
