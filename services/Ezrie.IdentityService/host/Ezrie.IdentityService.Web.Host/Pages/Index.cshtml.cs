using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Ezrie.IdentityService.Pages;

public class IndexModel : IdentityServicePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
