using Microsoft.EntityFrameworkCore;

namespace VoNguyenMinhNhat_KTGK.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<NganhHoc> NganhHocs { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<DangKy> DangKys { get; set; }
        public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SinhVien>().ToTable("SinhVien");
            modelBuilder.Entity<NganhHoc>().ToTable("NganhHoc");
            modelBuilder.Entity<HocPhan>().ToTable("HocPhan");
            modelBuilder.Entity<DangKy>().ToTable("DangKy");
            modelBuilder.Entity<ChiTietDangKy>().ToTable("ChiTietDangKy");

            // ✅ cấu hình khóa chính phức hợp
            modelBuilder.Entity<ChiTietDangKy>()
                .HasKey(c => new { c.MaDK, c.MaHP });
        }
    }
}
