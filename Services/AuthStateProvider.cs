using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSE_325_group_project.Services
{
    // custom authentication state provider to manage user authentication state
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJSRuntime _jsRuntime;

        // constructor takes in dependencies for HTTP requests and JavaScript interop
        public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime)
    {
        _httpClientFactory = httpClientFactory;
        _jsRuntime = jsRuntime;
    }

        // gets the current authentication state of the user
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                // prevent JS interop errors during prerendering
                // if (_jsRuntime is not IJSInProcessRuntime)
                // {
                //     // JSRuntime is not ready — return empty user
                //     return new AuthenticationState(new ClaimsPrincipal(identity));
                // }

                // retrieve the auth token from the browser
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                // if token is found and not empty parse claims from it
                if (!string.IsNullOrEmpty(token))
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error in GetAuthenticationStateAsync: {ex.Message}");
            }

            // create a ClaimsPrincipal with the identity
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        // sets the user as authenticated and notifies the system of the change
        public void MarkUserAsAuthenticated(string token)
        {
            Console.WriteLine("MarkUserAsAuthenticated called.");

            // parse claims from the provided JWT token
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            // Console.WriteLine($"User identity: {identity.Name}");

            // create a ClaimsPrincipal and notify the system
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        // marks the user as logged out and notifies the system
        public void MarkUserAsLoggedOut()
        {
            Console.WriteLine("MarkUserAsLoggedOut called.");

            // create an empty ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            // notify the system that the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        // parses claims from a JWT token
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            // JWT has 3 parts: header, payload & signature
            var payload = jwt.Split('.')[1]; // we only need the payload part

            var jsonBytes = WebEncoders.Base64UrlDecode(payload); // decode the base64url payload

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes); // deserialize to a dictionary

            // Console.WriteLine($"Parsed Claims: {string.Join(", ", keyValuePairs.Select(kvp => $"{kvp.Key}: {kvp.Value}"))}");

            // null checks
            if (keyValuePairs == null)
            {
                return Enumerable.Empty<Claim>();
            }

            // convert dictionary to list of Claim objects
            var claims = keyValuePairs
            .Where(kvp => kvp.Key != null && kvp.Value != null)
            .Select(kvp => new Claim(kvp.Key, kvp.Value!.ToString()!))
            .ToList();

            // if "name" claim is missing use the "sub" claim
            if (!claims.Any(c => c.Type == ClaimTypes.Name) && keyValuePairs.TryGetValue("sub", out var subValue) && subValue != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, subValue.ToString()!));
            }

            return claims;
        }

    }
}