using System.Linq;
using System.Net;
using Sitecore.Remote.Installation.Diagnostic;

namespace Sitecore.Remote.Installation.Extensions
{
  /// <summary>
  /// Extensions for WebHeaderCollection
  /// </summary>
  public static class WebHeaderCollectionExtensions
  {
    /// <summary>
    /// Copies to.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    public static void CopyTo(this WebHeaderCollection from, WebHeaderCollection to)
    {
      Assert.ArgumentNotNull(from, nameof(from));
      Assert.ArgumentNotNull(to, nameof(to));

      foreach (var key in from.AllKeys)
      {
        if (to.AllKeys.Contains(key))
        {
          to[key] = from[key];
        }

        to.Add(key, from[key]);
      }
    }
  }
}
