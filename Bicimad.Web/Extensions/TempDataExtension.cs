using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Bicimad.Enums;
using PagedList.Mvc;
using PagedList;
using HtmlHelper = System.Web.Mvc.HtmlHelper;

namespace Bicimad.Web.Extensions
{
    public static class TempDataExtensions
    {
        private const string ErrorKey = "MDWError";
        private const string MessageKey = "MDWMessage";

        public static void SetError(this TempDataDictionary tempData, ValidationResult validationResult)
        {
            tempData[ErrorKey] = validationResult;
        }

        public static bool HasError(this TempDataDictionary tempData)
        {
            return tempData[ErrorKey] != null;
        }

        public static ValidationResult GetError(this TempDataDictionary tempData)
        {
            return tempData[ErrorKey] as ValidationResult;
        }

        public static void SetMessage(this TempDataDictionary tempData, string message, MessageType messageType)
        {
            tempData[MessageKey] = new TempDataMessage
            {
                Message = message,
                MessageType = messageType
            };
        }

        public static bool HasMessage(this TempDataDictionary tempData)
        {
            return tempData[MessageKey] != null;
        }

        public static TempDataMessage GetMessage(this TempDataDictionary tempData)
        {
            return tempData[MessageKey] as TempDataMessage;
        }
    }

    public class TempDataMessage
    {
        public string Message { get; set; }
        public MessageType MessageType { get; set; }
    }
}