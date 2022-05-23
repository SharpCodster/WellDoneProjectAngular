using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using WellDoneProjectAngular.Core.Interfaces;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Core
{
    public class RequestContextProvider : IRequestContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RequestContext> GetRequestContexAsync()
        {
            HttpContext httpContext = GetHttpRequestContext();

            var user = await CreateUserFromRequestAsync(httpContext);

            return new RequestContext
            {
                User = user
            };
        }

        private HttpContext GetHttpRequestContext()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            //if (httpContext == null)
            //throw new NoRequestContextAvailableException();
            return httpContext;
        }

        private static async Task<ApplicationUser> CreateUserFromRequestAsync(HttpContext httpContext)
        {
            var user = httpContext.User;
            var userIdentity = user?.Identity;
            var userName = userIdentity?.Name;
            var userClaims = user?.Claims?.ToArray() ?? new Claim[] { };
            var userRoles = userClaims.Where(_ => _.Type == ClaimTypes.Role).Select(_ => _.Value).ToArray();
            var subject = "";

            //if (string.IsNullOrEmpty(userName))
            //{
            //    var identity = await JwtHelper.DecodeAccessTokenAsync(httpContext);
            //    subject = identity.Subject;
            //    userName = identity.UserName;
            //    userRoles = new string[] { identity.Role };
            //}

            return new ApplicationUser
            {
                UserName = userName
            };
        }

        public T GetQueryStringValue<T>(string parameterName)
        {
            var httpContext = GetHttpRequestContext();
            httpContext.Request.Query.TryGetValue(parameterName, out StringValues stringValues);

            if (stringValues == default(StringValues))
                return default(T);

            var value = stringValues.FirstOrDefault();

            if (value == null)
                return default(T);

            var result = Convert.ChangeType(value, typeof(T));

            return (T)result;
        }
    }
}
