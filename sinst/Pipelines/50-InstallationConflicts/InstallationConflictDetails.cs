using Sitecore.Remote.Installation.Client.Responses;
using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Installation conflict details
  /// </summary>
  public class InstallationConflictDetails
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallationConflictDetails"/> class.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="commandsResponse">The commands response.</param>
    public InstallationConflictDetails(IHttpClient client, CommandsResponse commandsResponse)
    {
      this.Client = client;
      this.CommandsResponse = commandsResponse;
    }

    /// <summary>
    /// Gets the client.
    /// </summary>
    /// <value>
    /// The client.
    /// </value>
    public IHttpClient Client { get; private set; }

    /// <summary>
    /// Gets or sets the commands response.
    /// </summary>
    /// <value>
    /// The commands response.
    /// </value>
    public CommandsResponse CommandsResponse { get; private set; }

    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    /// <value>
    /// The result.
    /// </value>
    public string Result { get; set; }

    /// <summary>
    /// Gets or sets the remote pipeline identifier.
    /// </summary>
    /// <value>
    /// The remote pipeline identifier.
    /// </value>
    public string RemotePipelineId { get; set; }
  }
}
