using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Ezrie.TenantService.Pages;

public class IndexModel : TenantServicePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
