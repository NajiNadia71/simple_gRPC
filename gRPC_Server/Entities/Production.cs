
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace gRPC_Server.Entities
{
    //[Table("Production")]
    public class Production
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public int ProductionTypeId { get; set; }
        public string CreateDate { get; set; }
        public string Comment { get; set; }
        public ProductionType ProductionType { get; set; }

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
    public class ProductionConfiguration : IEntityTypeConfiguration<Production>
    {
        public void Configure(EntityTypeBuilder<Production> builder)
        {
            builder.ToTable("Productions", "dbo");
            builder.HasKey(x => x.Id).HasName("PK_Productions");
            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Count).HasColumnName(@"Count").HasColumnType("int").IsRequired();
            builder.Property(x => x.ProductionTypeId).HasColumnName(@"ProductionTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.CreateDate).HasColumnName(@"CreateDate").HasColumnType("nvarchar(150)").IsRequired();
            builder.Property(x => x.Comment).HasColumnName(@"Comment").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Title).HasColumnName(@"Title").HasColumnType("nvarchar(50)").IsRequired().HasMaxLength(50);
            //Foregin key
            builder.HasOne(x => x.ProductionType).WithMany(p => p.Productions
            ).HasForeignKey(x => x.ProductionTypeId).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_ProductionTypes_Productions");


        }
    }

}


