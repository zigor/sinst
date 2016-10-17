using System.Net;

namespace Sitecore.Remote.Installation.Models
{
  /// <summary>
  /// Connection definition
  /// </summary>
  public interface IConnection
  {
    /// <summary>
    /// Gets or sets the host.
    /// </summary>
    /// <value>
    /// The host.
    /// </value>
    string Host { get; set; }

    /// <summary>
    /// Gets or sets the credentials.
    /// </summary>
    /// <value>
    /// The credentials.
    /// </value>
    NetworkCredential Credentials { get; set; }
  }
}