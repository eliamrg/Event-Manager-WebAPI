using Microsoft.AspNetCore.Mvc.Filters;

namespace Event_manager_API.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly ILogger<ActionFilter> log;

        public ActionFilter(ILogger<ActionFilter> log)
        {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("Before Excecuting acction");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("After Excecuting acction");
        }


    }
}
