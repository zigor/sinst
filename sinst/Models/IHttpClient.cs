using System.Collections.Specialized;

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
    /// <returns></returns>
    string Post(string url, NameValueCollection data);

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
  }
}