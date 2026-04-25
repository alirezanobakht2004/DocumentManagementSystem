using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ContractManager.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // مسیر ذخیره فایل دیتابیس (کنار فایل اجرایی برنامه)
            optionsBuilder.UseSqlite("Data Source=ContractManager.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
