using Xunit;

namespace Ezrie.IdentityService.Samples;

public class SampleManager_Tests : IdentityServiceDomainTestBase
{
    //private readonly SampleManager _sampleManager;

    public SampleManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

	[Fact]
	public Task Method1Async() => Task.CompletedTask;
}
