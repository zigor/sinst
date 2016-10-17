using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Sitecore.Remote.Installation.Diagnostic;

namespace Sitecore.Remote.Installation.Extensions
{
  /// <summary>
  /// Extensiosn for NameValueCollection
  /// </summary>
  public static class NameValueCollectionExtensions
  {
    /// <summary>
    /// Stringifies the specified name values.
    /// </summary>
    /// <param name="nameValues">The name values.</param>
    /// <returns></returns>
    public static string Stringify(this NameValueCollection nameValues)
    {
      Assert.ArgumentNotNull(nameValues, nameof(nameValues));

      StringBuilder builder = new StringBuilder();

      foreach (var key in nameValues.AllKeys)
      {
        builder.AppendFormat("{0}={1}&", key, nameValues[key]);
      }

      return builder.ToString(0, builder.Length - 1);
    }

    /// <summary>
    /// Copies to.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    /// <param name="includeKeys">The include keys.</param>
    public static void CopyTo(this NameValueCollection from, NameValueCollection to, params string[] includeKeys)
    {
      Assert.ArgumentNotNull(from, nameof(from));
      Assert.ArgumentNotNull(to, nameof(to));

      var keys = from.AllKeys.AsEnumerable();

      if (includeKeys != null)
      {
        keys = keys.Intersect(includeKeys);
      }

      foreach (var key in keys)
      {
        if (to.AllKeys.Any(k => k == key))
        {
          to[key] = from[key];
        }

        to.Add(key, from[key]);
      }
    }
  }
}
