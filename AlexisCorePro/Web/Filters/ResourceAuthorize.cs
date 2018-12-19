using AlexisCorePro.Business.Auth;
using AlexisCorePro.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Web;

namespace AlexisCorePro.Web.Filters
{
    public class ResourceAuthorize : ActionFilterAttribute
    {
        public string ResourceIdParamName { get; set; }
        
        public ResourceType ResourceType { get; set; }

        public ResourceAuthorize(string resourceIdParamName, ResourceType resourceType)
        {
            ResourceIdParamName = resourceIdParamName;
            ResourceType = resourceType;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? resourceId = null;

            PermissionService permissionService = (PermissionService)context.HttpContext.RequestServices.GetService(typeof(PermissionService));

            var queryParams = HttpUtility.ParseQueryString(context.HttpContext.Request.QueryString.ToString());

            if (queryParams[ResourceIdParamName] != null)
            {
                resourceId = int.Parse(queryParams[ResourceIdParamName]);
            }
            else
            {
                var path = context.HttpContext.Request.Path;
                resourceId = int.Parse(path.Value.Split('/').Last());
            }

            var isAuthorized = permissionService.IsUserAuthorized(resourceId, ResourceType);

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
