using System;
using Sitecore.Remote.Installation.Diagnostic;

namespace Sitecore.Remote.Installation.Extensions
{
  /// <summary>
  /// Extensions for Uri
  /// </summary>
  public static class UriExtensions
  {
    /// <summary>
    /// Gets the base URL.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <returns></returns>
    public static string GetBaseUrl(this Uri uri)
    {
      Assert.ArgumentNotNull(uri, nameof(uri));

      return uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
    }
  }
}
