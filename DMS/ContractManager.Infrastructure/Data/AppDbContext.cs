using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContractManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractMeeting> ContractMeetings { get; set; }
        public DbSet<ContractCorrespondence> ContractCorrespondences { get; set; }
        public DbSet<ContractTest> ContractTests { get; set; }
        public DbSet<ContractFinancialStatement> ContractFinancialStatements { get; set; }
        public DbSet<ContractDelivery> ContractDeliveries { get; set; }
        public DbSet<ContractDutyRelease> ContractDutyReleases { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<OCRResult> OCRResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. ایندکس‌گذاری برای ارتباط چندریختی (Polymorphic)
            modelBuilder.Entity<Attachment>()
                .HasIndex(a => new { a.RelatedEntityType, a.RelatedEntityId });

            // 2. تنظیم کلید خارجی برای OCRResult
            modelBuilder.Entity<OCRResult>()
                .HasOne<Attachment>()
                .WithMany()
                .HasForeignKey(o => o.AttachmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                            e.State == EntityState.Added ||
                            e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.Now;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entity.UpdatedAt = DateTime.Now;
                }
            }
        }
    }
}
