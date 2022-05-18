using Ezrie.AccountManagement.STS.Helpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ezrie.AccountManagement.STS.Helpers.TagHelpers;

[HtmlTargetElement("img-gravatar")]
public class GravatarTagHelper : TagHelper
{
	[HtmlAttributeName("email")]
	public String Email { get; set; }

	[HtmlAttributeName("alt")]
	public String Alt { get; set; }

	[HtmlAttributeName("class")]
	public String Class { get; set; }

	[HtmlAttributeName("size")]
	public Int32 Size { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		if (!String.IsNullOrWhiteSpace(Email))
		{
			var hash = Md5HashHelper.GetHash(Email);

			output.TagName = "img";
			if (!String.IsNullOrWhiteSpace(Class))
			{
				output.Attributes.Add("class", Class);
			}

			if (!String.IsNullOrWhiteSpace(Alt))
			{
				output.Attributes.Add("alt", Alt);
			}

			output.Attributes.Add("src", GetAvatarUrl(hash, Size));
			output.TagMode = TagMode.SelfClosing;
		}
	}

	private static String GetAvatarUrl(String hash, Int32 size)
	{
		var sizeArg = size > 0 ? $"?s={size}" : "";

		return $"https://www.gravatar.com/avatar/{hash}{sizeArg}";
	}
}

