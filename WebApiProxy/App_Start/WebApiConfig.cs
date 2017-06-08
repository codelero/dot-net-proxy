using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

using WebApiProxy.Handlers;
using WebApiProxy.Controllers;

namespace WebApiProxy
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //     name: "ConfigAzureAuth",
            //     routeTemplate: "config",
            //     defaults: new { controller = "ConfigAzureAuth" },
            //     constraints: new { isLocal = new HostConstraint { Host = "table.core.windows.net" } }
            // );



            // using DelegatingHandler
            //config.Routes.MapHttpRoute(
            //    name: "Proxy",
            //    routeTemplate: "zendesk/{*path}",
            //    handler: HttpClientFactory.CreatePipeline(
            //        innerHandler: new HttpClientHandler(), // will never get here if proxy is doing its job
            //        handlers: new DelegatingHandler[]
            //        { new ProxyHandler() }
            //    ),
            //    defaults: new { path = RouteParameter.Optional },
            //    constraints: null
            //);

            // using ProxyController works 
            config.Routes.MapHttpRoute(
                name: "Proxy2",
                routeTemplate: "zendesk/{controller}/{*path}",
                defaults: new { path = RouteParameter.Optional },
                constraints: null
            );

            // doesn't invoke ProxyController
            //config.Routes.MapHttpRoute(
            //    name: "Proxy",
            //    routeTemplate: "zendesk",
            //    defaults: new { controller = "Proxy" },
            //    constraints: null
            //);
        }

    }
}
