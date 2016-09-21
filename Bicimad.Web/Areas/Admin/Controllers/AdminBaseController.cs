using Bicimad.Web.Attributes;
using Bicimad.Web.Controllers;

namespace Bicimad.Web.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public partial class AdminBaseController : BaseController
    {
    }
}