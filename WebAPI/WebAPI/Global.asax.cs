﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAPI.Models;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Korisnici korisnici = new Korisnici("~/App_Data/TestFile.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            Dispeceri dispeceri = new Dispeceri("~/App_Data/Dispeceri.txt");
            HttpContext.Current.Application["dispeceri"] = dispeceri;
        }
    }
}