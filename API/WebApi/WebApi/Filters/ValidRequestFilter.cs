using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace WebApi.Filters
{
    public class ValidRequestAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new JsonResult
                (
                    new
                    {
                        Code = -1,
                        Msg = "Valid Error",
                        Result = context.ModelState.SelectMany(e => e.Value.Errors.Select(f => f.ErrorMessage))
                    }
                );
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
