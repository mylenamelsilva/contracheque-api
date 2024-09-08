using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Business.Base
{
    public class ExcecaoFiltro : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                message = context.Exception.Message,
                trace = context.Exception.StackTrace
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
