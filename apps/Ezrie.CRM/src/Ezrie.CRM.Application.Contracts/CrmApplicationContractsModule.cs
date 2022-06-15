﻿using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Ezrie.CRM;

[DependsOn(
    typeof(CRMDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CRMApplicationContractsModule : AbpModule
{

}
