using System.Web.Mvc;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Dto.History;
using Bicimad.Services.Query.Queries;
using PagedList;

namespace Bicimad.Web.Areas.User.Controllers
{
    public partial class HomeController : UserBaseController
    {
        private readonly UserHistoryQueryService _userStoryQueryService;

        public HomeController(UserHistoryQueryService userStoryQueryService)
        {
            _userStoryQueryService = userStoryQueryService;
        }

        public virtual ActionResult Index(int? page = null)
        {

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;

            var query = new UserHistoryQuery
            {
                Id = CurrentUser.Id,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var userHistorical = _userStoryQueryService.GetHistorial(ref query);

            var pagedUsers = new StaticPagedList<UserHistoryDto>(userHistorical, pageIndex + 1, pageSize, query.OutTotalCount);

            return View(MVC.User.Home.Views.Index, pagedUsers);
        }

    }

   
}