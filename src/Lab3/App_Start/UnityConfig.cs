using Services.Interfaces;
using Infrastructure.Model.Lab3;
using Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Lab3
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}