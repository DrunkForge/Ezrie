using Piranha.Extend;
using Piranha.Extend.Fields;

namespace Ezrie.CMS.Blocks;

[BlockGroupType(Name = "Accordion Content", Category = "Content", Icon = "fa fa-sort-circle", IsUnlisted = true, Component = "accordion-panel-block")]
public class AccordionItemBlock : Block
{
	public StringField Title { get; set; } = new() { Value = String.Empty };

	public HtmlField Body { get; set; } = new HtmlField() { Value = "[Section Content]" };

	public override String GetTitle()
		=> Title == null || String.IsNullOrWhiteSpace(Title.Value) ? "[Section Title]" : Title.Value;

	public String HtmlId => $"accordion-item-{Id.ToString().Replace("-", String.Empty)}";

	public String ParentId { get; private set; } = String.Empty;
	public Int32 Index { get; private set; }
	public String HeaderClass { get; private set; } = String.Empty;
	public String IconClass { get; private set; } = String.Empty;

	public void SetParameters(String parent, Int32 index, String header, String icon)
	{
		ParentId = parent;
		Index = index;
		HeaderClass = header;
		IconClass = icon;
	}
}
