using Piranha.AttributeBuilder;
using Piranha.Models;

namespace CPCA.Presentation.CMS.Models;

[PageType(Title = "Counsellor Page")]
[ContentTypeRoute(Title = "Counsellor", Route = "/cpca/counsellor")]
public class CounsellorPage : CmsPage<CounsellorPage>
{
}
