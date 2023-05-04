using Gama.Application.Contracts.UserManagement;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gama.Application.UseCases.UserManagement
{
    internal class HttpContextCurrentUserAcessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCurrentUserAcessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            if (claimsPrincipal == null)
            {
                throw new InvalidOperationException("Cannot retrieve user from null claims principal.");
            }

            var username = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return username?.Value ?? string.Empty;
        }
    }
}
