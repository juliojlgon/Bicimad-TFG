// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Bicimad.Web.Controllers
{
    public partial class BikeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BikeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult TakeBike()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TakeBike);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ReturnBike()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ReturnBike);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult BookBike()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BookBike);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RemoveBikeReservation()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveBikeReservation);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InformBrokenBike()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InformBrokenBike);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BikeController Actions { get { return MVC.Bike; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Bike";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Bike";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string ReturnIndex = "ReturnIndex";
            public readonly string TakeBike = "TakeBike";
            public readonly string ReturnBike = "ReturnBike";
            public readonly string BookBike = "BookBike";
            public readonly string RemoveBikeReservation = "RemoveBikeReservation";
            public readonly string InformBrokenBike = "InformBrokenBike";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string ReturnIndex = "ReturnIndex";
            public const string TakeBike = "TakeBike";
            public const string ReturnBike = "ReturnBike";
            public const string BookBike = "BookBike";
            public const string RemoveBikeReservation = "RemoveBikeReservation";
            public const string InformBrokenBike = "InformBrokenBike";
        }


        static readonly ActionParamsClass_TakeBike s_params_TakeBike = new ActionParamsClass_TakeBike();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_TakeBike TakeBikeParams { get { return s_params_TakeBike; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_TakeBike
        {
            public readonly string userId = "userId";
            public readonly string stationId = "stationId";
        }
        static readonly ActionParamsClass_ReturnBike s_params_ReturnBike = new ActionParamsClass_ReturnBike();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ReturnBike ReturnBikeParams { get { return s_params_ReturnBike; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ReturnBike
        {
            public readonly string userId = "userId";
            public readonly string stationId = "stationId";
        }
        static readonly ActionParamsClass_BookBike s_params_BookBike = new ActionParamsClass_BookBike();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_BookBike BookBikeParams { get { return s_params_BookBike; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_BookBike
        {
            public readonly string userId = "userId";
            public readonly string stationId = "stationId";
        }
        static readonly ActionParamsClass_RemoveBikeReservation s_params_RemoveBikeReservation = new ActionParamsClass_RemoveBikeReservation();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RemoveBikeReservation RemoveBikeReservationParams { get { return s_params_RemoveBikeReservation; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RemoveBikeReservation
        {
            public readonly string userId = "userId";
            public readonly string stationId = "stationId";
        }
        static readonly ActionParamsClass_InformBrokenBike s_params_InformBrokenBike = new ActionParamsClass_InformBrokenBike();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InformBrokenBike InformBrokenBikeParams { get { return s_params_InformBrokenBike; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InformBrokenBike
        {
            public readonly string bikeId = "bikeId";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_BikeController : Bicimad.Web.Controllers.BikeController
    {
        public T4MVC_BikeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ReturnIndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ReturnIndex()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ReturnIndex);
            ReturnIndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void TakeBikeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string userId, string stationId);

        [NonAction]
        public override System.Web.Mvc.ActionResult TakeBike(string userId, string stationId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TakeBike);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "stationId", stationId);
            TakeBikeOverride(callInfo, userId, stationId);
            return callInfo;
        }

        [NonAction]
        partial void ReturnBikeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string userId, string stationId);

        [NonAction]
        public override System.Web.Mvc.ActionResult ReturnBike(string userId, string stationId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ReturnBike);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "stationId", stationId);
            ReturnBikeOverride(callInfo, userId, stationId);
            return callInfo;
        }

        [NonAction]
        partial void BookBikeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string userId, string stationId);

        [NonAction]
        public override System.Web.Mvc.ActionResult BookBike(string userId, string stationId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BookBike);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "stationId", stationId);
            BookBikeOverride(callInfo, userId, stationId);
            return callInfo;
        }

        [NonAction]
        partial void RemoveBikeReservationOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string userId, string stationId);

        [NonAction]
        public override System.Web.Mvc.ActionResult RemoveBikeReservation(string userId, string stationId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RemoveBikeReservation);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "userId", userId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "stationId", stationId);
            RemoveBikeReservationOverride(callInfo, userId, stationId);
            return callInfo;
        }

        [NonAction]
        partial void InformBrokenBikeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string bikeId);

        [NonAction]
        public override System.Web.Mvc.ActionResult InformBrokenBike(string bikeId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InformBrokenBike);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "bikeId", bikeId);
            InformBrokenBikeOverride(callInfo, bikeId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114