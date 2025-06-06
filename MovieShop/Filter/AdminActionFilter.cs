using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

public class AdminActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Log.Information("Movie create starts on : " + DateTime.Now);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Log.Information("Movie created at: " + DateTime.Now);
    }
}