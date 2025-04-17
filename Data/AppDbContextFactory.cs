using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Adapte ici le chemin de ta base SQLite
            optionsBuilder.UseSqlite("Data Source=leave.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
