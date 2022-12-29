using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class PostReceiptFieldsConfiguration: IEntityTypeConfiguration<PostReceiptFields>
{
    public void Configure(EntityTypeBuilder<PostReceiptFields> builder)
    {
        builder.Property(p => p.ReceiptFieldId).IsRequired();
        builder.HasOne(b => b.Post).WithMany().HasForeignKey(p => p.PostId);
    }
}