using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonalPropertyApp.Models
{
    public partial class InsuranceAppContext : DbContext
    {
        public InsuranceAppContext()
        {
        }

        public InsuranceAppContext(DbContextOptions<InsuranceAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimItems> ClaimItems { get; set; }
        public virtual DbSet<ClaimReport> ClaimReport { get; set; }
        public virtual DbSet<PolicyDetails> PolicyDetails { get; set; }
        public virtual DbSet<PolicyHolder> PolicyHolder { get; set; }
        public virtual DbSet<PolicyItems> PolicyItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-966O9DU;Database=InsuranceApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ClaimItems>(entity =>
            {
                entity.HasKey(e => e.Claimitemid);

                entity.Property(e => e.Claimitemid).HasColumnName("claimitemid");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Reportid).HasColumnName("reportid");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ClaimItems)
                    .HasForeignKey(d => d.Itemid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClaimItems_PolicyItems");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ClaimItems)
                    .HasForeignKey(d => d.Reportid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClaimItems_ClaimReport");
            });

            modelBuilder.Entity<ClaimReport>(entity =>
            {
                entity.HasKey(e => e.Reportid);

                entity.Property(e => e.Reportid).HasColumnName("reportid");

                entity.Property(e => e.Claimreason)
                    .HasColumnName("claimreason")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Reportdate)
                    .HasColumnName("reportdate")
                    .HasColumnType("date");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.ClaimReport)
                    .HasForeignKey(d => d.Policyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClaimReport_PolicyDetails");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClaimReport)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClaimReport_PolicyHolder");
            });

            modelBuilder.Entity<PolicyDetails>(entity =>
            {
                entity.HasKey(e => e.Policyid);

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Inscompanyid).HasColumnName("inscompanyid");

                entity.Property(e => e.Inscompanyname)
                    .HasColumnName("inscompanyname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Inscompanywebsite)
                    .HasColumnName("inscompanywebsite")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Inscontactemail)
                    .HasColumnName("inscontactemail")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Inscontactname)
                    .HasColumnName("inscontactname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Inscontactphone)
                    .HasColumnName("inscontactphone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Policynumber)
                    .HasColumnName("policynumber")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PolicyDetails)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyDetails_PolicyHolder");
            });

            modelBuilder.Entity<PolicyHolder>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PolicyItems>(entity =>
            {
                entity.HasKey(e => e.Itemid);

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Itemcategory)
                    .HasColumnName("itemcategory")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Itemdescription)
                    .HasColumnName("itemdescription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Itemimage)
                    .HasColumnName("itemimage")
                    .HasColumnType("image");

                entity.Property(e => e.Itemname)
                    .IsRequired()
                    .HasColumnName("itemname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Policyid).HasColumnName("policyid");

                entity.Property(e => e.Purchasedate)
                    .HasColumnName("purchasedate")
                    .HasColumnType("date");

                entity.Property(e => e.Purchaseprice)
                    .HasColumnName("purchaseprice")
                    .HasColumnType("money");

                entity.Property(e => e.Receiptimage)
                    .HasColumnName("receiptimage")
                    .HasColumnType("image");

                entity.Property(e => e.Upc)
                    .HasColumnName("UPC")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyItems)
                    .HasForeignKey(d => d.Policyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyItems_PolicyDetails");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PolicyItems)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyItems_PolicyHolder");
            });
        }
    }
}
