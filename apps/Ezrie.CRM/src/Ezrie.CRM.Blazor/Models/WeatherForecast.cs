using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezrie.CRM.Models;

public class WeatherForecast
{
	public DateTime Date { get; set; }

	public Int32 TemperatureC { get; set; }

	public String? Summary { get; set; }

	public Int32 TemperatureF => 32 + (Int32)(TemperatureC / 0.5556);
}
