using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace Ezrie.CMS.Models;

public class CmsPage<T> : Page<T> where T : Page<T>
{
	[Region(Title = "Sidebar", Display = RegionDisplayMode.Setting)]
	public SelectField<SidebarPosition> Position { get; set; } = new SelectField<SidebarPosition>();

	[Region(Display = RegionDisplayMode.Content)]
	public IList<PageField> Sidebar { get; init; } = new List<PageField>();
}
