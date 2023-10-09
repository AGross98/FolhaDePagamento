using FolhaPag.Models;
using Microsoft.EntityFrameworkCore;

namespace FolhaPag.Data;
public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options){

    }
    //quais as classes vão se tornar tabelas no banco de dados.
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }

}
