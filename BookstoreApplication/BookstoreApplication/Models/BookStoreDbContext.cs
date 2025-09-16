using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            //V1
            modelBuilder.Entity<AuthorAward>()
                .HasOne(a => a.Author)
                .WithMany()
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);//V2

            modelBuilder.Entity<AuthorAward>()
                .HasOne(a => a.Award)
                .WithMany()
                .HasForeignKey(a => a.AwardId)
                .OnDelete(DeleteBehavior.Cascade);//V2
            //V2
            modelBuilder.Entity<AuthorAward>(entity =>
            {
                entity.ToTable("AuthorAwardBridge");
            });

            modelBuilder.Entity<Author>()
                .Property(a => a.DateOfBirth).HasColumnName("Birthday");

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)          
                .WithMany()                        
                .HasForeignKey(b => b.PublisherId) 
                .OnDelete(DeleteBehavior.Restrict);
            //V3

        }
    }
}
