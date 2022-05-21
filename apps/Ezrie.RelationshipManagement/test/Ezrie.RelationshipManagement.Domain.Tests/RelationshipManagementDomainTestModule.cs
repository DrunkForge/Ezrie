using Ezrie.RelationshipManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ezrie.RelationshipManagement;

[DependsOn(
    typeof(RelationshipManagementEntityFrameworkCoreTestModule)
    )]
public class RelationshipManagementDomainTestModule : AbpModule
{

}
