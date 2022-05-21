using System.Threading.Tasks;

namespace Ezrie.RelationshipManagement.Data;

public interface IRelationshipManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
