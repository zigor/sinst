using System.Collections.Specialized;
using System.Net;

namespace Sitecore.Remote.Installation.Models
{
  /// <summary>
  /// Http client definition
  /// </summary>
  public interface IHttpClient
  {
    /// <summary>
    /// Posts the specified URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="data">The data.</param>
    /// <param name="headers">The headers.</param>
    /// <returns></returns>
    string Post(string url, NameValueCollection data, NameValueCollection headers = null);

    /// <summary>
    /// Gets the specified URL.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    string Get(string url);

    /// <summary>
    /// Gets the headers.
    /// </summary>
    /// <value>
    /// The headers.
    /// </value>
    NameValueCollection Headers { get; }

    /// <summary>
    ///   Gets the connection.
    /// </summary>
    /// <value>
    ///   The connection.
    /// </value>
    IConnection Connection { get; }

    /// <summary>
    /// Gets the cookie.
    /// </summary>
    /// <value>
    /// The cookie.
    /// </value>
    CookieContainer Cookie { get; }
  }
}