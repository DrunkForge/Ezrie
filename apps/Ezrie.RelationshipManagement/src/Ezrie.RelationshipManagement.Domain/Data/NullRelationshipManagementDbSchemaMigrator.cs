using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ezrie.RelationshipManagement.Data;

/* This is used if database provider does't define
 * IRelationshipManagementDbSchemaMigrator implementation.
 */
public class NullRelationshipManagementDbSchemaMigrator : IRelationshipManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
