using Ford_Env_Service.Controllers;
using Ford_Env_Service.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace Ford_Env_Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //container.RegisterType<DAL.Contract.DAL_Contract, DAL.Implementation.SQL_Implementation>();
            container.RegisterType<DAL.Contract.DAL_Contract, DAL.Implementation.DynamoDBImplementation>();


            //container.RegisterType<UserManager<ApplicationUser>, ApplicationUserManager>();
            //container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<ApplicationUserManager>();
            //container.RegisterType<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            //container.RegisterType<ITextEncoder, Base64UrlTextEncoder>();
            //container.RegisterType<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(accountInjectionConstructor);

            container.RegisterType<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            
            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}