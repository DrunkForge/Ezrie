using Ezrie.CRM.Localization;
using Volo.Abp.Application.Services;

namespace Ezrie.CRM;

public abstract class CRMAppService : ApplicationService
{
    protected CRMAppService()
    {
        LocalizationResource = typeof(CRMResource);
        ObjectMapperContext = typeof(CRMApplicationModule);
    }
}
