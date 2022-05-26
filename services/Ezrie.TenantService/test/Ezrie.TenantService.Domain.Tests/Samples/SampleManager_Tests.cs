using Xunit;

namespace Ezrie.TenantService.Samples;

public class SampleManager_Tests : TenantServiceDomainTestBase
{
	//private readonly SampleManager _sampleManager;

	public SampleManager_Tests()
	{
		//_sampleManager = GetRequiredService<SampleManager>();
	}

	[Fact]
	public Task Method1Async() => Task.CompletedTask;
}
