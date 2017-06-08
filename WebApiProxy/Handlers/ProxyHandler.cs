using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApiProxy.Handlers
{
    public class ProxyHandler: DelegatingHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancelationToken)
        {
            var requestAbsolutePath = request.RequestUri.AbsolutePath;

            var routeTemplate = "/zendesk/";

            requestAbsolutePath = requestAbsolutePath.Substring(routeTemplate.Length);

            UriBuilder forwardUri = new UriBuilder("https://fitsme1491919531.zendesk.com/api/v2/" + requestAbsolutePath);
            //UriBuilder forwardUri = new UriBuilder("http://google.com/" + requestAbsolutePath);

            forwardUri.Port = 443;

            request.RequestUri = forwardUri.Uri;

            HttpClient client = new HttpClient();

            if (request.Method == HttpMethod.Get) { request.Content = null; }

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            return response;
        }
    }
}