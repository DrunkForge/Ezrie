using Piranha;
using Piranha.Extend;
using Piranha.Extend.Fields;
using System.Text.RegularExpressions;

namespace CPCA.Presentation.CMS.Blocks;

[BlockType(Name = "Card", Category = "Content", Icon = "fas fa-rectangle-vertical-history fa-rotate-270", Component = "card-block")]
public class CardBlock : Block
{
	public ImageField Image { get; set; } = new();

	public StringField Title { get; set; } = new() { Value = String.Empty };
	
	public StringField SubTitle { get; set; } = new() { Value = String.Empty };

	public HtmlField Content { get; set; } = new() { Value = String.Empty };

	public PageField Link { get; set; } = new();

	public StringField Button { get; set; } = new() { Value = String.Empty };

	public override String GetTitle()
		=> Title == null || String.IsNullOrWhiteSpace(Title.Value)
			? base.GetTitle()
			: Title.Value;

	/// <summary>
	/// Gets the content that should be indexed for searching.
	/// </summary>
	public String GetIndexedContent()
		=> (String.IsNullOrEmpty(Title.Value) ? String.Empty : Title.Value)
		 + (String.IsNullOrEmpty(Content.Value) ? String.Empty : Content.Value);
}
