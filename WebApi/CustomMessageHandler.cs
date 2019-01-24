using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public class CustomMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestBody = await request.Content.ReadAsStringAsync();
            HttpResponseMessage responseMessage;
            if (request.Method != HttpMethod.Post)
            {
                responseMessage = new HttpResponseMessage {StatusCode = HttpStatusCode.MethodNotAllowed};
            }
            else
            {
                Console.WriteLine(requestBody);
                responseMessage = await base.SendAsync(request, cancellationToken);
            }

            return responseMessage;
        }
    }
}