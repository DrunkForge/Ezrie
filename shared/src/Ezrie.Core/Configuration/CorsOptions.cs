using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.Configuration;
public class CorsOptions
{
	public Boolean CorsAllowAnyOrigin { get; set; }
	[SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Arrays play nice with IConfiguration")]
	public String[] CorsAllowOrigins { get; set; } = Array.Empty<String>();
}
