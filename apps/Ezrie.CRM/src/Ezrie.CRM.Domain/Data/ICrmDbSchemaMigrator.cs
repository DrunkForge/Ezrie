using System.Threading.Tasks;

namespace Ezrie.CRM.Data;

public interface ICrmDbSchemaMigrator
{
	Task MigrateAsync();
}
