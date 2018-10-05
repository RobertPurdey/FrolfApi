using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Frolf.Api.MessageHandlers
{
    public class GeneralExceptionHandler : ExceptionHandler
    {
        private class GeneralExceptionResult : IHttpActionResult
        {
            private HttpResponseMessage Response { get; }

            public GeneralExceptionResult(HttpResponseMessage response)
            {
                Response = response;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Response);
            }
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            // HttpResponseException are automatically bypassed by the
            // Web API pipeline. Exceptions that are handled by the
            // exception filters will also not be processed.
            var exceptionMessage = new StringBuilder();

            exceptionMessage.AppendLine("todo: Make General Exception Message Resource");
            exceptionMessage.AppendLine(context.Exception.ToString());

            context.Result = new GeneralExceptionResult(
                new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exceptionMessage.ToString())
                });
        }
    }
}