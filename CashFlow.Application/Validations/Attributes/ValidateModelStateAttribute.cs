using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Application.Validations.Attributes
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        //private readonly INotificationContext _notificationContext;

        public ValidateModelStateAttribute(/*INotificationContext notificationContext*/)
        {
            //_notificationContext = notificationContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage);
                foreach (var error in errors)
                {
                    //_notificationContext.AddNotification(
                    //    StatusCodes.Status400BadRequest.ToString(),
                    //    ApplicationMessage.MODELO_INVALIDO,
                    //    error);
                }

                //context.Result = new JsonResult(_notificationContext)
                //{
                //    StatusCode = StatusCodes.Status400BadRequest
                //};
            }
        }
    }
}
