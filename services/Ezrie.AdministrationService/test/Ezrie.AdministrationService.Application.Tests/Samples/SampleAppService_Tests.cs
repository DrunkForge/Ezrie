using FluentAssertions;
using Xunit;

namespace Ezrie.AdministrationService.Samples;

public class SampleAppService_Tests : AdministrationServiceApplicationTestBase
{
	private readonly ISampleAppService _sampleAppService;

	public SampleAppService_Tests()
	{
		_sampleAppService = GetRequiredService<ISampleAppService>();
	}

	[Fact]
	public async Task GetAsync()
	{
		var result = await _sampleAppService.GetAsync();
		result.Value.Should().Be(42);
	}

	[Fact]
	public async Task GetAuthorizedAsync()
	{
		var result = await _sampleAppService.GetAuthorizedAsync();
		result.Value.Should().Be(42);
	}
}
