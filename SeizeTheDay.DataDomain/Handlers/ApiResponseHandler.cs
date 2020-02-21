using SeizeTheDay.DataDomain.Common;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SeizeTheDay.DataDomain.Handlers
{
    public class ApiResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content = null;
            string errorMessage = string.Empty;

            ValidateResponse(response, ref content, ref errorMessage);

            // Yeni response'u custom olarak oluşturmuş olduğumuz wrapper sınıf ile baştan oluşturuyoruz.
            var newResponse = CreateHttpResponseMessage(request, response, content, errorMessage);

            // Header key'lerini baştan set et.
            foreach (var loopHeader in response.Headers)
            {
                newResponse.Headers.Add(loopHeader.Key, loopHeader.Value);
            }

            return newResponse;
        }

        private static HttpResponseMessage CreateHttpResponseMessage<T>(HttpRequestMessage request, HttpResponseMessage response, T content, string errorMessage)
        {
            return request.CreateResponse(response.StatusCode, new ApiResponse<T>(response.StatusCode, content, errorMessage));
        }


        private static void ValidateResponse(HttpResponseMessage response, ref object content, ref string errorMessage)
        {
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;
                    StringBuilder sb = new StringBuilder();

                    foreach (var loopError in error)
                    {
                        sb.Append(string.Format("{0}: {1} ", loopError.Key, loopError.Value));
                    }

                    errorMessage = sb.ToString();
                }
            }
        }
    }
}
