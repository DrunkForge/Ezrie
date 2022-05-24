using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ezrie.TenantService.EntityFrameworkCore;

[ConnectionStringName(TenantServiceDbProperties.ConnectionStringName)]
public interface ITenantServiceDbContext : IEfCoreDbContext
{
	/* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
