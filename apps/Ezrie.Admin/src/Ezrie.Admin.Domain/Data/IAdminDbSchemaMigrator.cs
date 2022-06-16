using System.Threading.Tasks;

namespace Ezrie.Admin.Data;

public interface IAdminDbSchemaMigrator
{
    Task MigrateAsync();
}
