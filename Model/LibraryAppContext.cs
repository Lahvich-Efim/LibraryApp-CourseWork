using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Model;

public partial class LibraryAppContext : DbContext
{
    public LibraryAppContext()
    {
    }

    public LibraryAppContext(DbContextOptions<LibraryAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Dictionary> Dictionaries { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<Notify> Notifies { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SpecValueForProductList> SpecValueForProductLists { get; set; }

    public virtual DbSet<Specification> Specifications { get; set; }

    public virtual DbSet<SpecificationValue> SpecificationValues { get; set; }

    public virtual DbSet<TypeBook> TypeBooks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=PETYA;Database=LibraryApp;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
    }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

            entity.HasOne(d => d.PaliasNavigation).WithMany(p => p.Authors)
                .HasForeignKey(d => d.Palias)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Author_Dictionary");
        });

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Baskets_UserId").IsUnique();

            entity.HasOne(d => d.User).WithOne(p => p.Basket)
                .HasForeignKey<Basket>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Baskets_Users");

            entity.HasMany(d => d.ProductCodes).WithMany(p => p.Baskets)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductBasket",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductBa__Produ__15DA3E5D"),
                    l => l.HasOne<Basket>().WithMany()
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductBasket_Baskets"),
                    j =>
                    {
                        j.HasKey("BasketId", "ProductCode").HasName("PK__ProductB__7D2E97F117C03CC2");
                        j.ToTable("ProductBasket");
                        j.HasIndex(new[] { "ProductCode" }, "IX_ProductBasket_ProductID");
                        j.IndexerProperty<int>("BasketId").HasColumnName("BasketID");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B4A8A1B77");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Pimage)
                .HasMaxLength(255)
                .HasColumnName("PImage");
            entity.Property(e => e.Pname).HasColumnName("PName");

            entity.HasOne(d => d.PnameNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.Pname)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Dictionary");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.CollectionId).HasName("PK__Collecti__7DE6BC24064CCBA2");

            entity.HasIndex(e => e.LibraryId, "IX_Collections");

            entity.Property(e => e.CollectionId).HasColumnName("CollectionID");
            entity.Property(e => e.LibraryId).HasColumnName("LibraryID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Library).WithMany(p => p.Collections)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Collections_Libraries");

            entity.HasMany(d => d.ProductCodes).WithMany(p => p.Collections)
                .UsingEntity<Dictionary<string, object>>(
                    "CollectionProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Collectio__Produ__68487DD7"),
                    l => l.HasOne<Collection>().WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Collectio__Colle__6754599E"),
                    j =>
                    {
                        j.HasKey("CollectionId", "ProductCode").HasName("PK__Collecti__8F125C00D9880147");
                        j.ToTable("CollectionProducts");
                        j.HasIndex(new[] { "ProductCode" }, "IX_CollectionProducts_ProductId");
                        j.IndexerProperty<int>("CollectionId").HasColumnName("CollectionID");
                    });
        });

        modelBuilder.Entity<Dictionary>(entity =>
        {
            entity.HasKey(e => e.WordId);

            entity.ToTable("Dictionary");

            entity.Property(e => e.WordEn).HasMaxLength(255);
            entity.Property(e => e.WordRus).HasMaxLength(255);
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.LibraryId).HasName("PK__Librarie__A13647BF1B1D0772");

            entity.HasIndex(e => e.UserId, "User-ID").IsUnique();

            entity.Property(e => e.LibraryId).HasColumnName("LibraryID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.Library)
                .HasForeignKey<Library>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libraries_Users");
        });

        modelBuilder.Entity<Notify>(entity =>
        {
            entity.HasKey(e => new { e.IdNotify, e.IdUser });

            entity.ToTable("Notify");

            entity.Property(e => e.IdNotify).ValueGeneratedOnAdd();
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.DataNotify).HasColumnType("datetime");
            entity.Property(e => e.Header).HasMaxLength(100);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Notifies)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notify_Users");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("OrderInfo");

            entity.HasIndex(e => e.UserId, "IX_OrderInfo_UserID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderInfo_Users");

            entity.HasMany(d => d.ProductCodes).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderItem",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderItem__Produ__22401542"),
                    l => l.HasOne<OrderInfo>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__OrderItem__Order__214BF109"),
                    j =>
                    {
                        j.HasKey("OrderId", "ProductCode").HasName("PK__OrderIte__3164BB8BE65595F0");
                        j.ToTable("OrderItems");
                        j.HasIndex(new[] { "OrderId" }, "IX_OrderItems_OrderID");
                        j.IndexerProperty<int>("OrderId").HasColumnName("OrderID");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductCode).HasName("PK__Products__2F4E024E08D3FBD7");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CatalogId).HasColumnName("CatalogID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescriptionEn).IsUnicode(false);
            entity.Property(e => e.DescriptionRus).IsUnicode(false);
            entity.Property(e => e.Pimage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PImage");
            entity.Property(e => e.Pname).HasColumnName("PName");

            entity.HasOne(d => d.Author).WithMany(p => p.Products)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Author");

            entity.HasOne(d => d.Catalog).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catalo__7E37BEF6");

            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Publisher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Publisher");

            entity.HasOne(d => d.TypeBookNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_TypeBook");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.IdPublisher);

            entity.ToTable("Publisher");

            entity.Property(e => e.IdPublisher).HasColumnName("idPublisher");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE56F7AB31");

            entity.HasIndex(e => e.ProductCode, "IX_Reviews_ProductID");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Product__0E6E26BF");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserID__0F624AF8");
        });

        modelBuilder.Entity<SpecValueForProductList>(entity =>
        {
            entity.HasKey(e => new { e.SpecValueId, e.ProductId, e.SpecId });

            entity.ToTable("SpecValueForProduct_List");

            entity.Property(e => e.SpecValueId).HasColumnName("specValueId");
            entity.Property(e => e.SpecId).HasColumnName("specId");

            entity.HasOne(d => d.Product).WithMany(p => p.SpecValueForProductLists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecValueForProduct_List_Products");

            entity.HasOne(d => d.Spec).WithMany(p => p.SpecValueForProductLists)
                .HasForeignKey(d => d.SpecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecValueForProduct_List_Specifications");

            entity.HasOne(d => d.SpecValue).WithMany(p => p.SpecValueForProductLists)
                .HasForeignKey(d => d.SpecValueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SpecValueForProduct_List_SpecificationValues");
        });

        modelBuilder.Entity<Specification>(entity =>
        {
            entity.HasKey(e => e.SpecId);

            entity.HasOne(d => d.PspecNameNavigation).WithMany(p => p.Specifications)
                .HasForeignKey(d => d.PspecName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Specifications_Dictionary");

            entity.HasMany(d => d.ProductCodes).WithMany(p => p.Specs)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductSpecification",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductSp__Produ__55009F39"),
                    l => l.HasOne<Specification>().WithMany()
                        .HasForeignKey("SpecId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductSp__SpecI__540C7B00"),
                    j =>
                    {
                        j.HasKey("SpecId", "ProductCode").HasName("PK__ProductS__7AC9B65FDEEB6FA0");
                        j.ToTable("ProductSpecifications");
                        j.HasIndex(new[] { "ProductCode" }, "IX_ProductSpecifications_ProductId");
                    });
        });

        modelBuilder.Entity<SpecificationValue>(entity =>
        {
            entity.HasKey(e => e.SpecValueId).HasName("PK_SpecificationValues_1");

            entity.Property(e => e.SpecValueId).HasColumnName("specValueId");
            entity.Property(e => e.PvalueString).HasColumnName("PValueString");
            entity.Property(e => e.SpecId).HasColumnName("specID");
            entity.Property(e => e.ValueInt).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.PvalueStringNavigation).WithMany(p => p.SpecificationValues)
                .HasForeignKey(d => d.PvalueString)
                .HasConstraintName("FK_SpecificationValues_Dictionary");
        });

        modelBuilder.Entity<TypeBook>(entity =>
        {
            entity.HasKey(e => e.IdType);

            entity.ToTable("TypeBook");

            entity.Property(e => e.IdType).HasColumnName("idType");

            entity.HasOne(d => d.PnameNavigation).WithMany(p => p.TypeBooks)
                .HasForeignKey(d => d.Pname)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TypeBook_Dictionary");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACFFE494F3");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4EE4B2AEE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053498F4D6C8").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FatherName).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.Pavatar)
                .HasMaxLength(255)
                .HasColumnName("PAvatar");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Salt).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
