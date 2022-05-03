using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Xapp.Web.Utilities
{
    public static class AlertExtensions
    {
        private static IActionResult Alert(IActionResult result, string type, string title, string body, string buttonText)
        {
            return new AlertDecoratorResult(result, type, title, body, buttonText);
        }

        public static IActionResult WithSuccess(this IActionResult result, string title, string body, string buttonText )
        {
            return Alert(result, "success", title, body, buttonText );
        }

        public static IActionResult WithDanger(this IActionResult result, string title, string body, string buttonText )
        {
            return Alert(result, "error", title, body, buttonText );
        }

    }
    public class AlertDecoratorResult : IActionResult
    {
        public IActionResult Result { get; }
        public string Type { get; }
        public string Title { get; }
        public string Body { get; }
        public string ButtonText { get; }

        public AlertDecoratorResult(IActionResult result, string type, string title, string body, string buttonText)
        {
            Result = result;
            Type = type;
            Title = title;
            Body = body;
            ButtonText = buttonText;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var factory = context.HttpContext.RequestServices.GetService<ITempDataDictionaryFactory>();

            var tempData = factory.GetTempData(context.HttpContext);
            tempData["_alert.type"] = Type;
            tempData["_alert.title"] = Title;
            tempData["_alert.body"] = Body;
            tempData["_alert.buttonText"] = ButtonText;
            await Result.ExecuteResultAsync(context);
        }
    }
}
