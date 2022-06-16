﻿using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Ezrie.Admin;

public class AdminTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */

        return Task.CompletedTask;
    }
}