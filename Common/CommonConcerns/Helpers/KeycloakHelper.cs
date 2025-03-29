using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace CommonConcerns.Helpers;

// keycloak kullanıcı rolünü kontrol etmek için helper class. Keylcloak
public static class KeycloakHelper
{
	public static bool UserHasRealmRole(ClaimsPrincipal user, string role)
	{
		var realmRolesClaim = user.Claims.FirstOrDefault(c => c.Type == "realm_access");

		if (realmRolesClaim == null)
			return false;

		try
		{
			var roles = JsonDocument.Parse(realmRolesClaim.Value)
				.RootElement.GetProperty("roles")
				.EnumerateArray()
				.Select(role => role.GetString());

			return roles.Contains(role);
		}
		catch (JsonException)
		{
			return false;
		}
	}
}


