
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace Ezrie.AccountManagement.STS.ViewModels.Diagnostics;

public class DiagnosticsViewModel
{
	public DiagnosticsViewModel(AuthenticateResult result)
	{
		AuthenticateResult = result;

		if (result.Properties.Items.ContainsKey("client_list"))
		{
			var encoded = result.Properties.Items["client_list"];
			var bytes = Base64Url.Decode(encoded);
			var value = Encoding.UTF8.GetString(bytes);

			Clients = JsonConvert.DeserializeObject<String[]>(value);
		}
	}

	public AuthenticateResult AuthenticateResult { get; }
	public IEnumerable<String> Clients { get; } = new List<String>();
}

