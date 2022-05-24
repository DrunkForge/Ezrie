using Piranha.AttributeBuilder;
using Piranha.Models;

namespace CPCA.Presentation.CMS.Models;

[PageType(Title = "Directory Page")]
[ContentTypeRoute(Title = "Directory", Route = "/cpca/directory")]
public class DirectoryPage : CmsPage<DirectoryPage>
{
}
