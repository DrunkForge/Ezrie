using Ezrie;
using Ezrie.Logging;
using Serilog;

internal static class Program
{
	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
	public async static Task<Int32> Main(String[] args)
	{
		try
		{
			var builder = WebApplication.CreateBuilder();

			builder.Host.AddAppSettingsSecretsJson()
				.UseAutofac()
				.AddAppSettingsSecretsJson()
				.UseEzrieLogging<EzrieCmsModule>();

			await builder.Build().RunAsync().ConfigureAwait(false);

			return 0;
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Host terminated unexpectedly!");
			return 1;
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}
