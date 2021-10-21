using GO.Service.Users.Enums;

namespace GO.Service.Users.Helpers
{
    internal static class ScopeHelper
    {
        public static Scopes GetScopes(Roles role)
        {
            var scopes = Scopes.None;

            if (role.HasFlag(Roles.User))
                scopes |= Scopes.Movies;

            if (role.HasFlag(Roles.Administration))
                scopes |= Scopes.Support;

            return scopes;
        }
    }
}
