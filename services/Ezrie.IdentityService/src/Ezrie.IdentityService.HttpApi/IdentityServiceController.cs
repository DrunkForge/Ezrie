﻿using Ezrie.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezrie.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
