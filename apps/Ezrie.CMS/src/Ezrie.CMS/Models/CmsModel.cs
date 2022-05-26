using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using Piranha.Models;

namespace Ezrie.CMS.Models;

public class CmsModel<T> : Microsoft.AspNetCore.Mvc.RazorPages.PageModel where T : PageBase
{
	public CmsModel(IApi api, IModelLoader loader)
	{
		Api = api;
		Loader = loader;
	}

	protected IApi Api { get; }
	protected IModelLoader Loader { get; }

	public T Data { get; set; } = default!;
	public Boolean HasHeaderImage => Data.PrimaryImage.HasValue;
	public String BgCss => HasHeaderImage ? " bg-image" : "bg-no-image";
	public String BgStyle => HasHeaderImage ? $" style=background-image:url({ Url.Content(Data.PrimaryImage) })" : "";
	public String Title => !String.IsNullOrEmpty(Data.MetaTitle) ? Data.MetaTitle : Data.Title;

	/// <summary>
	/// Gets the model data.
	/// </summary>
	/// <param name="id">The requested model id</param>
	/// <param name="draft">If the draft should be fetched</param>
	public virtual async Task<IActionResult> OnGet(Guid id, Boolean draft = false, CancellationToken cancellationToken = default)
	{
		try
		{
			Data = await Loader.GetPageAsync<T>(id, HttpContext.User, draft);

			if (Data == null)
			{
				return NotFound();
			}

			ViewData["Title"] = Title;

			return await OnHttpGet(cancellationToken);
		}
		catch (UnauthorizedAccessException)
		{
			return Unauthorized();
		}
	}

	protected virtual Task<IActionResult> OnHttpGet(CancellationToken cancellationToken = default)
		=> Task.FromResult((IActionResult)Page());
	protected virtual Task<IActionResult> OnHttpPost(CancellationToken cancellationToken = default)
		=> Task.FromResult((IActionResult)Page());
}
