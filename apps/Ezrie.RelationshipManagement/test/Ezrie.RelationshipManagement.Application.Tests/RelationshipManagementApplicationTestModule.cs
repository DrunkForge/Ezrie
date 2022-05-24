using Volo.Abp.Modularity;

namespace Ezrie.RelationshipManagement;

[DependsOn(
	typeof(RelationshipManagementApplicationModule),
	typeof(RelationshipManagementDomainTestModule)
	)]
public class RelationshipManagementApplicationTestModule : AbpModule
{

}
