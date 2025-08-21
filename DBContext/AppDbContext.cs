using KhoBau_BE.Entities;
using Microsoft.EntityFrameworkCore;

namespace KhoBau_BE.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<BaiToanModel> BaiToans => Set<BaiToanModel>();
        public DbSet<KetQuaModel> KetQuas => Set<KetQuaModel>();
    }
}