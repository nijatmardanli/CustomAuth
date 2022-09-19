using Auth.WebApi.Utilities.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Auth.WebApi.Utilities.Authentication
{
    public class XAuthenticationHandler : AuthenticationHandler<XAuthenticationSchemeOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public XAuthenticationHandler(IOptionsMonitor<XAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor httpContextAccessor) : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim("UserId", "123", ClaimValueTypes.Integer32)
            }, XAuthDefaults.DefaultSchemeName);

            var claimsPrincipal = new ClaimsPrincipal(identity);


            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, XAuthDefaults.DefaultSchemeName))); //Ok
            //return Task.FromResult(AuthenticateResult.Fail("Token is invalid")); //Unauthorized
            //return Task.FromResult(AuthenticateResult.NoResult());

        }
    }
}
