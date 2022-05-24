using CPCA.Presentation.CMS.Blocks;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Text;

namespace CPCA.Presentation.CMS.Pages.DisplayTemplates;

/// <summary>
/// Single column quote block.
/// </summary>
[BlockGroupType(Name = "Accordion", Category = "Content", Icon = "fa fa-layer-group")]
[BlockItemType(Type = typeof(AccordionItemBlock))]
public class AccordionBlock : BlockGroup, ISearchable
{
	[Field(Options = FieldOption.HalfWidth)]
	public CheckBoxField Inverse { get; set; } = new();

	[Field(Options = FieldOption.HalfWidth)]
	public SelectField<HorizontalAlignment> Alignment { get; set; } = new SelectField<HorizontalAlignment>() { Value = HorizontalAlignment.Right };

	/// <summary>
	/// Gets the content that should be indexed for searching.
	/// </summary>
	public String GetIndexedContent()
	{
		var content = new StringBuilder();

		foreach (var item in Items)
		{
			if (item is ISearchable searchItem)
			{
				var value = searchItem.GetIndexedContent();

				if (!String.IsNullOrEmpty(value))
				{
					content.AppendLine(value);
				}
			}
		}

		return content.ToString();
	}

	public IEnumerable<AccordionItemBlock> Panels()
	{
		for (var index = 0; index < Items.Count; index++)
		{
			if (Items[index] is AccordionItemBlock panel)
			{
				panel.SetParameters(HtmlId(), index, Header, Icon);
				yield return panel;
			}
		}
	}

	public String HtmlId() => $"accordion-{Id.ToString().Replace("-", String.Empty)}";

	private String Header => Inverse.Value ? " header-inverse " : String.Empty;

	private String Icon => Alignment.Value switch
	{
		HorizontalAlignment.Left => " icon-left ",
		_ => String.Empty,
	};
}
