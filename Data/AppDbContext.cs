using Microsoft.EntityFrameworkCore;
using ThanhToanTienNuoc.Models;

namespace ThanhToanTienNuoc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<DiaChi> DiaChis { get; set; }
        public DbSet<ChiSoNuoc> ChiSoNuocs { get; set; }
        //     public DbSet<WaterUsageQuarter> WaterUsageQuarters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminAccount>(entity =>
            {
                entity.HasKey(e => e.AdminID);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang);
            });
          /*  modelBuilder.Entity<WaterUsageQuarter>(entity =>
            {
                entity.HasKey(e => e.UsageId);
                entity.HasOne<NguoiDungs>()
                      .WithMany(wu => wu.WaterUsageQuarters)
                      .HasForeignKey(wuq => wuq.WaterUserId);
            });*/
        }
    }
}