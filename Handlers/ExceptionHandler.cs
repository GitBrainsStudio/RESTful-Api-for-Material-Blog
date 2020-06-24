using GitBrainsBlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitBrainsBlogApi.Handlers
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        readonly Error _error = new Error();
        public void OnException(ExceptionContext context)
        {
            _error.Write(context.Exception.ToString());
            string errorMessage = "Произошла непредвиденная ошибка в приложении. Администрация сайта уже бежит на помощь.";
            if (context.Exception is HumanException) errorMessage = context.Exception.Message;
            context.Result = new BadRequestObjectResult(errorMessage);
        }
    }
}
