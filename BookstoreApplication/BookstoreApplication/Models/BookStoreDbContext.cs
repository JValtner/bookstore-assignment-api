using BookstoreApplication.Models.ExternalComics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Models
{
    public class BookStoreDbContext: IdentityDbContext<ApplicationUser>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; }
        public DbSet<LocalIssue> LocalIssues { get; set; }
        public DbSet<ComicVineImage> ComicVineImages { get; set; }
        public DbSet<ComicVineVolume> ComicVineVolumes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
              new IdentityRole { Name = "Librarian", NormalizedName = "LIBRARIAN" },
              new IdentityRole { Name = "Editor", NormalizedName = "EDITOR" }
            );
            modelBuilder.Entity<LocalIssue>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.OwnsOne(i => i.Volume, vol =>
                {
                    vol.Property(v => v.Id).HasColumnName("Volume_Id");
                    vol.Property(v => v.Name).HasColumnName("Volume_Name");
                    vol.Property(v => v.Api_detail_url).HasColumnName("Volume_ApiDetailUrl");
                });

                entity.OwnsOne(i => i.Image, img =>
                {
                    img.Property(v => v.Icon_url).HasColumnName("Image_IconUrl");
                    img.Property(v => v.Medium_url).HasColumnName("Image_MediumUrl");
                    img.Property(v => v.Super_url).HasColumnName("Image_SuperUrl");
                });
            });

            //V4
            modelBuilder.Entity<Book>()
            .Property(e => e.PublishedDate)
            .HasConversion(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Author>()
            .Property(e => e.DateOfBirth)
            .HasConversion(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            
            modelBuilder.Entity<AuthorAward>()
            .Property(e => e.AwardedDate)
            .HasConversion(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));


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

            // --- Authors ---
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Ivo Andrić", Biography = "Dobitnik Nobelove nagrade za književnost.", DateOfBirth = DateTime.SpecifyKind(new DateTime(1892, 10, 9), DateTimeKind.Utc) },
                new Author { Id = 2, FullName = "Mesa Selimović", Biography = "Autor romana Derviš i smrt.", DateOfBirth = DateTime.SpecifyKind(new DateTime(1910, 4, 26), DateTimeKind.Utc) },
                new Author { Id = 3, FullName = "Danilo Kiš", Biography = "Poznat po romanu Bašta, pepeo.", DateOfBirth = DateTime.SpecifyKind(new DateTime(1935, 2, 22), DateTimeKind.Utc) },
                new Author { Id = 4, FullName = "Branko Ćopić", Biography = "Pisac za decu i odrasle, humorista.", DateOfBirth = DateTime.SpecifyKind(new DateTime(1915, 1, 1), DateTimeKind.Utc) },
                new Author { Id = 5, FullName = "Dobrica Ćosić", Biography = "Prozaista i političar.", DateOfBirth = DateTime.SpecifyKind(new DateTime(1921, 12, 29), DateTimeKind.Utc) }
            );

            // --- Publishers ---
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Laguna", Address = "Beograd, Srbija", Website = "https://www.laguna.rs" },
                new Publisher { Id = 2, Name = "Vulkan", Address = "Beograd, Srbija", Website = "https://www.knjizare-vulkan.rs" },
                new Publisher { Id = 3, Name = "Dereta", Address = "Beograd, Srbija", Website = "https://www.dereta.rs" }
            );

            // --- Awards ---
            modelBuilder.Entity<Award>().HasData(
                new Award { Id = 1, Name = "NIN-ova nagrada", Description = "Najznačajnija književna nagrada u Srbiji.", YearEstablished = 1954 },
                new Award { Id = 2, Name = "Nobelova nagrada za književnost", Description = "Međunarodna nagrada za književnost.", YearEstablished = 1901 },
                new Award { Id = 3, Name = "Andrićeva nagrada", Description = "Dodeljuje se za priče i romane.", YearEstablished = 1975 },
                new Award { Id = 4, Name = "Zmajeva nagrada", Description = "Nagrada za dečju književnost.", YearEstablished = 1958 }
            );

            // --- Books ---
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Na Drini ćuprija", PageCount = 350, PublishedDate = DateTime.SpecifyKind(new DateTime(1945, 1, 1), DateTimeKind.Utc), ISBN = "978000000001", AuthorId = 1, PublisherId = 1 },
                new Book { Id = 2, Title = "Travnička hronika", PageCount = 320, PublishedDate = DateTime.SpecifyKind(new DateTime(1945, 1, 1), DateTimeKind.Utc), ISBN = "978000000002", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 3, Title = "Derviš i smrt", PageCount = 400, PublishedDate = DateTime.SpecifyKind(new DateTime(1966, 1, 1), DateTimeKind.Utc), ISBN = "978000000003", AuthorId = 2, PublisherId = 1 },
                new Book { Id = 4, Title = "Tvrđava", PageCount = 380, PublishedDate = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc), ISBN = "978000000004", AuthorId = 2, PublisherId = 3 },
                new Book { Id = 5, Title = "Bašta, pepeo", PageCount = 250, PublishedDate = DateTime.SpecifyKind(new DateTime(1965, 1, 1), DateTimeKind.Utc), ISBN = "978000000005", AuthorId = 3, PublisherId = 2 },
                new Book { Id = 6, Title = "Rani jadi", PageCount = 200, PublishedDate = DateTime.SpecifyKind(new DateTime(1969, 1, 1), DateTimeKind.Utc), ISBN = "978000000006", AuthorId = 3, PublisherId = 1 },
                new Book { Id = 7, Title = "Peščanik", PageCount = 300, PublishedDate = DateTime.SpecifyKind(new DateTime(1972, 1, 1), DateTimeKind.Utc), ISBN = "978000000007", AuthorId = 3, PublisherId = 2 },
                new Book { Id = 8, Title = "Ježeva kućica", PageCount = 50, PublishedDate = DateTime.SpecifyKind(new DateTime(1949, 1, 1), DateTimeKind.Utc), ISBN = "978000000008", AuthorId = 4, PublisherId = 3 },
                new Book { Id = 9, Title = "Doživljaji mačka Toše", PageCount = 60, PublishedDate = DateTime.SpecifyKind(new DateTime(1955, 1, 1), DateTimeKind.Utc), ISBN = "978000000009", AuthorId = 4, PublisherId = 1 },
                new Book { Id = 10, Title = "Koreni", PageCount = 420, PublishedDate = DateTime.SpecifyKind(new DateTime(1954, 1, 1), DateTimeKind.Utc), ISBN = "978000000010", AuthorId = 5, PublisherId = 2 },
                new Book { Id = 11, Title = "Deobe", PageCount = 500, PublishedDate = DateTime.SpecifyKind(new DateTime(1961, 1, 1), DateTimeKind.Utc), ISBN = "978000000011", AuthorId = 5, PublisherId = 1 },
                new Book { Id = 12, Title = "Vreme smrti", PageCount = 600, PublishedDate = DateTime.SpecifyKind(new DateTime(1972, 1, 1), DateTimeKind.Utc), ISBN = "978000000012", AuthorId = 5, PublisherId = 3 }
            );

            // --- AuthorAward ---
            modelBuilder.Entity<AuthorAward>().HasData(
                new AuthorAward { Id = 1, AuthorId = 1, AwardId = 2, AwardedDate = DateTime.SpecifyKind(new DateTime(1961, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 2, AuthorId = 1, AwardId = 3, AwardedDate = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 3, AuthorId = 2, AwardId = 1, AwardedDate = DateTime.SpecifyKind(new DateTime(1967, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 4, AuthorId = 2, AwardId = 3, AwardedDate = DateTime.SpecifyKind(new DateTime(1971, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 5, AuthorId = 3, AwardId = 1, AwardedDate = DateTime.SpecifyKind(new DateTime(1966, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 6, AuthorId = 3, AwardId = 3, AwardedDate = DateTime.SpecifyKind(new DateTime(1973, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 7, AuthorId = 3, AwardId = 4, AwardedDate = DateTime.SpecifyKind(new DateTime(1975, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 8, AuthorId = 4, AwardId = 4, AwardedDate = DateTime.SpecifyKind(new DateTime(1950, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 9, AuthorId = 4, AwardId = 3, AwardedDate = DateTime.SpecifyKind(new DateTime(1960, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 10, AuthorId = 4, AwardId = 1, AwardedDate = DateTime.SpecifyKind(new DateTime(1965, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 11, AuthorId = 5, AwardId = 1, AwardedDate = DateTime.SpecifyKind(new DateTime(1955, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 12, AuthorId = 5, AwardId = 2, AwardedDate = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 13, AuthorId = 5, AwardId = 3, AwardedDate = DateTime.SpecifyKind(new DateTime(1978, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 14, AuthorId = 2, AwardId = 4, AwardedDate = DateTime.SpecifyKind(new DateTime(1975, 1, 1), DateTimeKind.Utc) },
                new AuthorAward { Id = 15, AuthorId = 1, AwardId = 4, AwardedDate = DateTime.SpecifyKind(new DateTime(1978, 1, 1), DateTimeKind.Utc) }
            );
            // --- Indexes for performance ---
            modelBuilder.Entity<Author>()
                .HasIndex(a => a.FullName)                 // search/filter by author name
                .HasDatabaseName("IX_Author_FullName");

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Title)                    // search/filter by book title
                .HasDatabaseName("IX_Book_Title");

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.AuthorId)                 // improve joins on Author
                .HasDatabaseName("IX_Book_AuthorId");

            modelBuilder.Entity<AuthorAward>()
                .HasIndex(aa => new { aa.AuthorId, aa.AwardId })  // avoid duplicates
                .HasDatabaseName("IX_AuthorAward_Author_Award");


        }
    }
}
