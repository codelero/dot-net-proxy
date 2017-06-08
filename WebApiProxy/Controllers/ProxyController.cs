using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiProxy.Controllers
{

    public class ProxyController : ApiController
    {
        [HttpGet, HttpPut]
        public async Task<HttpResponseMessage> HandleProxy(string path, [FromBody]string value)
        {

            HttpRequestMessage request = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;

            //var absoluteUri = request.RequestUri.AbsoluteUri;

            //var routeTemplate = "http://localhost:51804/zendesk/proxy/";

            //absoluteUri = absoluteUri.Substring(routeTemplate.Length);

            UriBuilder forwardUri = new UriBuilder("https://fitsme1491919531.zendesk.com/api/v2/help_center/articles/search.json?section=115000925985");
            //UriBuilder forwardUri = new UriBuilder("https://api.github.com/users/octocat");

            //forwardUri.Port = 443;

            request.RequestUri = forwardUri.Uri;

            //    request.Headers.Authorization = new AuthenticationHeaderValue(
            //        "Basic",
            //Convert.ToBase64String(
            //    System.Text.ASCIIEncoding.ASCII.GetBytes(
            //        string.Format("{0}:{1}", "mzol14@gmail.com", "testpassword"))));

            // if you comment this out you'll get the trust error
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };



            HttpClient client = new HttpClient();


            if (request.Method == HttpMethod.Get) { request.Content = null; }

            var response = await client.SendAsync(request);

            return response;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
