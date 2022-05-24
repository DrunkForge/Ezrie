﻿using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ezrie.RelationshipManagement.HttpApi.Client.ConsoleTestApp;

class Program
{
	static async Task Main(String[] args)
	{
		await CreateHostBuilder(args).RunConsoleAsync();
	}

	public static IHostBuilder CreateHostBuilder(String[] args) =>
		Host.CreateDefaultBuilder(args)
			.AddAppSettingsSecretsJson()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddHostedService<ConsoleTestAppHostedService>();
			});
}
