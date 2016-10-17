using System.Net;

namespace Sitecore.Remote.Installation.Models
{
  /// <summary>
  /// Connection details
  /// </summary>
  /// <seealso cref="Sitecore.Remote.Installation.Models.IConnection" />
  public class Connection : IConnection
  {
    /// <summary>
    ///   Gets or sets the host.
    /// </summary>
    /// <value>
    ///   The host.
    /// </value>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the credentials.
    /// </summary>
    /// <value>
    /// The credentials.
    /// </value>
    public NetworkCredential Credentials { get; set; }
  }
}