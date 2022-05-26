using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Ezrie.IdentityService.EntityFrameworkCore;

public static class IdentityServiceDbContextModelCreatingExtensions
{
	public static void CreateIdentityServiceModel(this ModelBuilder modelBuilder) {
	
		modelBuilder.ConfigureIdentityService();
		modelBuilder.ConfigureIdentity();

	}

	public static void ConfigureIdentityService(this ModelBuilder modelBuilder)
    {
        Check.NotNull(modelBuilder, nameof(modelBuilder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(IdentityServiceDbProperties.DbTablePrefix + "Questions", IdentityServiceDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
