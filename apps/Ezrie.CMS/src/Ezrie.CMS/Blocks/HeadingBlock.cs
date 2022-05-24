using Piranha.Extend;
using Piranha.Extend.Fields;

namespace CPCA.Presentation.CMS.Blocks;

[BlockType(Name = "Heading", Category = "Content", Icon = "fas fa-heading", Component = "heading-block")]
public class HeadingBlock : Block
{
	public StringField Heading { get; set; } = new StringField() { Value = String.Empty };

	public TextField Description { get; set; } = new TextField() { Value = String.Empty };

	public SelectField<HorizontalAlignment> Alignment { get; set; } = new SelectField<HorizontalAlignment>() { Value = HorizontalAlignment.Center };

	public override String GetTitle()
	{
		if (Heading != null && !String.IsNullOrWhiteSpace(Heading.Value))
			return Heading.Value;

		return base.GetTitle();
	}

	/// <summary>
	/// Gets the content that should be indexed for searching.
	/// </summary>
	public String GetIndexedContent()
		=> (String.IsNullOrEmpty(Heading.Value) ? String.Empty : Heading.Value)
		 + (String.IsNullOrEmpty(Description.Value) ? String.Empty : Description.Value);

	public String RowAlignment() => Alignment.Value switch
	{
		HorizontalAlignment.Center => "justify-content-md-center",
		HorizontalAlignment.Left => "justify-content-md-start",
		HorizontalAlignment.Right => "justify-content-md-end",
		_ => String.Empty,
	};

	public String TextAlignment() => Alignment.Value switch
	{
		HorizontalAlignment.Center => "text-center",
		HorizontalAlignment.Left => "text-left",
		HorizontalAlignment.Right => "text-right",
		_ => String.Empty,
	};
}
