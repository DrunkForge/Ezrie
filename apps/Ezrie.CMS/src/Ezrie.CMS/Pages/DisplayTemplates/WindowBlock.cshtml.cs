using Piranha.Extend;
using Piranha.Extend.Fields;
using System.Text.RegularExpressions;

namespace Ezrie.CMS.Pages.DisplayTemplates;

/// <summary>
/// Single column HTML block.
/// </summary>
[BlockType(Name = "Fixed Background", Category = "Content", Icon = "fas fa-window-maximize", Component = "window-block")]
public class WindowBlock : Block, ISearchable, ITranslatable
{
	/// <summary>
	/// Gets/sets the HTML body.
	/// </summary>
	public HtmlField Body { get; set; } = new();

	public ImageField Image { get; set; } = new();

	public NumberField Height { get; set; }

	/// <summary>
	/// Gets the title of the block when used in a block group.
	/// </summary>
	/// <returns>The title</returns>
	public override String GetTitle()
	{
		if (Body?.Value != null)
		{
			var title = Regex.Replace(Body.Value, @"<[^>]*>", "");

			if (title.Length > 40)
			{
				title = title.Substring(0, 40) + "...";
			}

			return title;
		}
		return "Empty";
	}

	/// <summary>
	/// Gets the content that should be indexed for searching.
	/// </summary>
	public String GetIndexedContent()
	{
		return !String.IsNullOrEmpty(Body.Value) ? Body.Value : "";
	}

	/// <summary>
	/// Implicitly converts the Html block to a string.
	/// </summary>
	/// <param name="block">The block</param>
	public static implicit operator String(WindowBlock block)
	{
		return block.Body?.Value;
	}
}
