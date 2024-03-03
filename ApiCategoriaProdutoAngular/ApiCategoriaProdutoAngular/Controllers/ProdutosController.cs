using ApiCategoriaProdutoAngular.Context;
using ApiCategoriaProdutoAngular.Models;
using Microsoft.AspNetCore.Http;
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
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _context.Produtos
            .AsNoTracking()
            .ToList();

        return Ok(produtos);    
    }

    [HttpPost]
    public ActionResult<Produto> PostProduto(Produto produto) 
    {
        _context.Produtos.Add(produto);

        _context.SaveChanges();

        return produto;
    }

    [HttpPut("{produtoId}")]   
    public ActionResult<Produto> PutProduto(Produto produto, string produtoId)
    {
        Guid id;

        if (!Guid.TryParse(produtoId, out id))
        {
            return BadRequest("Não foi possível converter o GUI em ID");
        }

        produto.ProdutoId = id; 

        _context.Produtos.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();

        return Ok(produto); 
    }

    [HttpDelete("{produtoId}")]
    public ActionResult<Produto> DeleteProduto(string produtoId)
    {
        var produto = _context.Produtos
            .FirstOrDefault(produto => produto.ProdutoId
            .ToString() == produtoId);

        if (produto == null)
        {
            return BadRequest("Id não encontrado");
        }

        _context.Produtos.Remove(produto);

        _context.SaveChanges(); 

        return produto;
    }
}
