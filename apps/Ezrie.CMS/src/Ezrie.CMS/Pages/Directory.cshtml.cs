using CPCA.Application.Directory;
using CPCA.Application.Members;
using CPCA.Presentation.CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Piranha;
using Piranha.AspNetCore.Services;
using System.Text.RegularExpressions;

namespace CPCA.Presentation.CMS.Pages;

public class DirectoryModel : CmsModel<DirectoryPage>
{
	private readonly IDirectoryService _directory;
	private readonly IMemoryCache _cache;

	public DirectoryModel(IApi api, IDirectoryService directory, IMemoryCache cache, IModelLoader loader) : base(api, loader)
	{
		_directory = directory;
		_cache = cache;
	}

	[FromRoute(Name = "key")]
	public String Key { get; set; }

	[FromQuery(Name = "s")]
	public Int32 Skip { get; set; }
	[FromQuery(Name = "t")]
	public Int32 Take { get; set; }

	[BindProperty]
	public String Keywords { get; set; }
	[BindProperty]
	public Int32 ApproachId { get; set; }
	[BindProperty]
	public Int32 IssueId { get; set; }
	[BindProperty]
	public Int32 TypeId { get; set; }

	public SelectList Approaches { get; set; }
	public SelectList Issues { get; set; }
	public SelectList Types { get; set; }

	public DirectorySearchResultDto Results { get; set; } = DirectorySearchResultDto.Empty;

	protected override async Task<IActionResult> OnHttpGet(CancellationToken cancellationToken)
	{
		await LoadSelects(cancellationToken);

		if (Key != null && Regex.IsMatch(Key, "^[a-zA-Z]$"))
		{
			Results = await _directory.FindMembersByLastInitialAsync(Key[0], Skip, Take, cancellationToken);
		}

		return Page();
	}

	private async Task LoadSelects(CancellationToken cancellationToken)
	{
		var lookups = await _cache.GetOrCreate(CpcaCms.CacheKeys.DirectoryLookups, async entry =>
		{
			entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
			return await _directory.GetLookupTablesAsync(cancellationToken);
		});

		Approaches = new SelectList(lookups.Aproaches, "Id", "Name", ApproachId);
		Issues = new SelectList(lookups.Issues, "Id", "Name", IssueId);
		Types = new SelectList(lookups.Types, "Id", "Name", TypeId);
	}

	public virtual async Task<IActionResult> OnPost(Guid id, Boolean draft = false, CancellationToken cancellationToken = default)
	{
		try
		{
			Data = await _loader.GetPageAsync<DirectoryPage>(id, HttpContext.User, draft);

			if (Data == null)
			{
				return NotFound();
			}

			ViewData["Title"] = Title;

			await LoadSelects(cancellationToken);

			return Page();
		}
		catch (UnauthorizedAccessException)
		{
			return Unauthorized();
		}
	}
}
