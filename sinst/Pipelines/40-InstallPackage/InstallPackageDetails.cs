using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Upload package details
  /// </summary>
  public class InstallPackageDetails
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallPackageDetails"/> class.
    /// </summary>
    /// <param name="package">The package.</param>
    /// <param name="client">The client.</param>
    public InstallPackageDetails(string package, IHttpClient client)
    {
      this.Package = package;
      this.Client = client;
    }

    /// <summary>
    /// Gets the package.
    /// </summary>
    /// <value>
    /// The package.
    /// </value>
    public string Package { get; }

    /// <summary>
    /// Gets or sets the client.
    /// </summary>
    /// <value>
    /// The client.
    /// </value>
    public IHttpClient Client { get; }
  }
}
