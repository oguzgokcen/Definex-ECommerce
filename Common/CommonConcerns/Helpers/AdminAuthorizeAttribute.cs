using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CommonConcerns.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AdminAuthorizeAttribute : TypeFilterAttribute
{
	public AdminAuthorizeAttribute() : base(typeof(AdminAuthorizationFilter)) { }

	private class AdminAuthorizationFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (context.HttpContext.User == null || !KeycloakHelper.UserHasRealmRole(context.HttpContext.User, "admin"))
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}
}

