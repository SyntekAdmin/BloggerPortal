using Microsoft.Rest.TransientFaultHandling;
using Stis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Mvc5;

namespace F21.BLOGGER.WebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            FrameContext.Current.Build();
            DependencyResolver.SetResolver(new UnityDependencyResolver(FrameContext.Current.Container));
        }
    }
}