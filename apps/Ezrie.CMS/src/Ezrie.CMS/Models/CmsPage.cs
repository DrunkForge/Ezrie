﻿using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace CPCA.Presentation.CMS.Models;

public class CmsPage<T> : Page<T> where T : Page<T>
{
	[Region(Title = "Sidebar", Display = RegionDisplayMode.Setting)]
	public SelectField<SidebarPosition> Position { get; set; }

	[Region(Display = RegionDisplayMode.Content)]
	public IList<PageField> Sidebar { get; set; }
}