namespace Arkanis.Overlay.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

internal class DevelopmentUexDbContextFactory : IDesignTimeDbContextFactory<UEXContext>
{
    public UEXContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UEXContext>();
        optionsBuilder.UseSqlite("Data Source=:memory:");

        return new UEXContext(optionsBuilder.Options);
    }
}
