using ApiCategoriaProdutoAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCategoriaProdutoAngular.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Produto> Produtos { get; set; }

    public DbSet<AuthUser> Users { get; set; }

    public DbSet<CreateUser> UsersCreated { get; set; }
}
