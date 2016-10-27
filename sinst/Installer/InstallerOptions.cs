using System;
using System.Net;
using Sitecore.Remote.Installation.Installer.Events;

namespace Sitecore.Remote.Installation.Installer
{
  /// <summary>
  ///   Installer options
  /// </summary>
  public class InstallerOptions
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallerOptions" /> class.
    /// </summary>
    /// <param name="host">The host.</param>
    /// <param name="credentials">The credentials.</param>
    public InstallerOptions(string host, NetworkCredential credentials)
    {
      this.Host = host;
      this.Credentials = credentials;

      this.Silent = true;
      this.SilentOptionsSet = "2|3|True;continue;yes to all";
    }

    /// <summary>
    ///   Gets or sets the host.
    /// </summary>
    /// <value>
    ///   The host.
    /// </value>
    public string Host { get; }

    /// <summary>
    ///   Gets or sets the credentials.
    /// </summary>
    /// <value>
    ///   The credentials.
    /// </value>
    public NetworkCredential Credentials { get; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="InstallerOptions"/> is silent.
    /// </summary>
    /// <value>
    ///   <c>true</c> if silent; otherwise, <c>false</c>.
    /// </value>
    public bool Silent { get; set; }

    /// <summary>
    /// Gets or sets the silent options set.
    /// </summary>
    /// <value>
    /// The silent options set.
    /// </value>
    public string SilentOptionsSet { get; set; }

    /// <summary>
    ///   The input
    /// </summary>
    public EventHandler<InputRequiredEventArgs> Input;

    /// <summary>
    /// The output
    /// </summary>
    public EventHandler<OutputRequredEventArgs> Output;
  }
}