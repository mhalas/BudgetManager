using BudgetManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.DataAccess;

public class BudgetManagerContext(string connectionString): DbContext
{
    public DbSet<Category> Category { get; set; }
    public DbSet<CategoryItem> CategoryItem { get; set; }
    public DbSet<BankTransaction> BankTransaction { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}