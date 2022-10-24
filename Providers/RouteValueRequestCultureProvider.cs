using Microsoft.AspNetCore.Localization;

namespace Internationalization.Providers;

// <summary>
/// Determines the culture information for a request via values in the route values.
/// </summary>
public class RouteValueRequestCultureProvider : RequestCultureProvider
{
    /// <summary>
    /// The key that contains the culture name.
    /// Defaults to "culture".
    /// </summary>
    public string RouteValueKey { get; set; } = "culture";

    /// <summary>
    /// The key that contains the UI culture name. If not specified or no value is found,
    /// <see cref="RouteValueKey"/> will be used.
    /// Defaults to "ui-culture".
    /// </summary>
    public string UIRouteValueKey { get; set; } = "ui-culture";

    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }

        var request = httpContext.Request;
        if (!request.RouteValues.Any())
        {
            return NullProviderCultureResult;
        }

        string? queryCulture = null;
        string? queryUICulture = null;

        if (!string.IsNullOrWhiteSpace(RouteValueKey))
        {
            queryCulture = request.RouteValues[RouteValueKey]?.ToString();
        }

        if (!string.IsNullOrWhiteSpace(UIRouteValueKey))
        {
            queryUICulture = request.RouteValues[UIRouteValueKey]?.ToString();
        }

        if (queryCulture == null && queryUICulture == null)
        {
            // No values specified for either so no match
            return NullProviderCultureResult;
        }

        if (queryCulture != null && queryUICulture == null)
        {
            // Value for culture but not for UI culture so default to culture value for both
            queryUICulture = queryCulture;
        }
        else if (queryCulture == null && queryUICulture != null)
        {
            // Value for UI culture but not for culture so default to UI culture value for both
            queryCulture = queryUICulture;
        }

        var providerResultCulture = new ProviderCultureResult(queryCulture, queryUICulture);

        return Task.FromResult<ProviderCultureResult?>(providerResultCulture);
    }
}
