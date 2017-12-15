using System.Web.Mvc;

namespace iBusMvcSystem.Areas.RepositoryPattern_ADO
{
    public class RepositoryPattern_ADOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RepositoryPattern_ADO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RepositoryPattern_ADO_default",
                "RepositoryPattern_ADO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}