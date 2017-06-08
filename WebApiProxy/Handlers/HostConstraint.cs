using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebApiProxy.Handlers
{
    public class HostConstraint: IHttpRouteConstraint
    {
        public string Host { get; set; }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            return request.RequestUri.Host.Contains(Host);
        }
    }
}