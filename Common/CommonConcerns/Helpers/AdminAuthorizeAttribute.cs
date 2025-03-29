using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

