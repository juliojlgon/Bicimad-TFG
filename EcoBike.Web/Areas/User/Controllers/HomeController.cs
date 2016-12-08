using System.Web.Mvc;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Dto.History;
using Bicimad.Services.Query.Dto.Reservation;
using Bicimad.Services.Query.Queries;
using PagedList;

namespace Bicimad.Web.Areas.User.Controllers
{
    public partial class HomeController : UserBaseController
    {
        private readonly UserHistoryQueryService _userStoryQueryService;
        private readonly ReservationQueryService _reservationQueryService;

        public HomeController(UserHistoryQueryService userStoryQueryService, ReservationQueryService reservationQueryService)
        {
            _userStoryQueryService = userStoryQueryService;
            _reservationQueryService = reservationQueryService;
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

        public virtual ActionResult ActiveRerservations(int? page = null)
        {

            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;

            var query = new ReservationQuery
            {
                Id = CurrentUser.Id,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var reservationHistorical = _reservationQueryService.GetReservations(ref query);

            var pagedUsers = new StaticPagedList<ReservationDto>(reservationHistorical, pageIndex + 1, pageSize, query.OutTotalCount);

            return View(MVC.User.Home.Views.Reservations, pagedUsers);
        }

    }

   
}