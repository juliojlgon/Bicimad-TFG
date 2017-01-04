using System;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using HtmlHelper = System.Web.Mvc.HtmlHelper;

namespace Bicimad.Web.Extensions
{
    public static class HtmlHelperExtension
    {

        public static MvcHtmlString GenericPager(this HtmlHelper helper, IPagedList list, Func<int, string> url)
        {
            return list == null ? new MvcHtmlString(string.Empty) : helper.PagedListPager(list, url, BasicPagerOptions());
        }

        private static PagedListRenderOptions BasicPagerOptions()
        {
            return new PagedListRenderOptions
            {
                Display = PagedListDisplayMode.IfNeeded,
                MaximumPageNumbersToDisplay = 10,
                DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
                DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
                LinkToNextPageFormat = ">>",
                LinkToPreviousPageFormat = "<<",
                LinkToFirstPageFormat = "Primera",
                LinkToLastPageFormat = "Última",
                DisplayEllipsesWhenNotShowingAllPageNumbers = true
            };
        }
    }
}