namespace Ezrie;

using FluentAssertions;
using Xunit;

public class StringExtensionTests
{
	[Fact]
	public void Redact()
	{
		Action act1 = () => StringExtensions.Redact(null!);
		act1.Should().Throw<ArgumentNullException>();

		Action act2 = () => StringExtensions.Redact("", -5);
		act2.Should().Throw<ArgumentOutOfRangeException>();

		"".Redact().Should().Be("");
		"pa".Redact().Should().Be("**");
		"pass".Redact().Should().Be("****");
		"password".Redact().Should().Be("****word");
	}
}
