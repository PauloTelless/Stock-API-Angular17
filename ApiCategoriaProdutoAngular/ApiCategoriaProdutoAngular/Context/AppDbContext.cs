using ApiCategoriaProdutoAngular.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCategoriaProdutoAngular.Context;

public class AppDbContext : IdentityDbContext<AplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
    }

}
