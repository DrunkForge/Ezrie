using CPCA.Application;
using CPCA.Application.Directory;
using CPCA.Application.Members;
using CPCA.Presentation.CMS.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Piranha;
using Piranha.AspNetCore.Services;
using System.Web;

namespace CPCA.Presentation.CMS.Pages;

public class CounsellorModel : CmsModel<CounsellorPage>
{
	private readonly IDirectoryService _directory;
	private readonly IMemoryCache _cache;

	public CounsellorModel(IApi api, IDirectoryService directory, IMemoryCache cache, IModelLoader loader)
		: base(api, loader)
	{
		_directory = directory;
		_cache = cache;
	}

	[FromRoute(Name = "code")]
	public Int32 MemberNumber { get; set; }

	[FromRoute(Name = "name")]
	public String MemberName { get; set; }

	private FullMemberDto Member { get; set; } = FullMemberDto.Empty;

	public HtmlString FullName => new(HttpUtility.HtmlEncode(Member.FullName));
	public HtmlString Designation => new(HttpUtility.HtmlEncode(Member.MemberType.Designation()));
	public HtmlString PhotoUrl => Member.PhotoUrl();
	public HtmlString Email => new(HttpUtility.HtmlEncode(Member.DirectoryListing.Email));
	public HtmlString EmailUrl => Member.EmailUrl();
	public HtmlString Phone => new(HttpUtility.HtmlEncode(Member.DirectoryListing.Phone.FormatPhone()));
	public HtmlString PhoneUrl => Member.TelephoneUrl();
	public Boolean HasWebsite => Member.HasWebsite();
	public HtmlString Website => new(HttpUtility.HtmlEncode(Member.DirectoryListing.Website));
	public HtmlString WebsiteUrl => Member.WebsiteUrl();
	public HtmlString Profile => Member.DirectoryListing.Text.RenderMarkdown();

	public IReadOnlyCollection<String> CounsellingApproaches => Member.CounsellingApproaches.Select(e => e.Name).ToArray();
	public IReadOnlyCollection<String> CounsellingIssues => Member.CounsellingIssues.Select(e => e.Name).ToArray();
	public IReadOnlyCollection<String> CounsellingTypes => Member.CounsellingTypes.Select(e => e.Name).ToArray();

	protected override async Task<IActionResult> OnHttpGet(CancellationToken cancellationToken = default)
	{
		if (MemberNumber >= CpcaCms.MemberNumberMin)
		{
			Member = await _directory.GetMemberMyMembershipNumberAsync(MemberNumber, cancellationToken);
			var lookups = await _cache.GetOrCreate(CpcaCms.CacheKeys.DirectoryLookups, async entry =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
				return await _directory.GetLookupTablesAsync(cancellationToken);
			});
		}
		else
		{
			return Redirect(CpcaCms.DirectoryUrl);
		}

		return Page();
	}
}
