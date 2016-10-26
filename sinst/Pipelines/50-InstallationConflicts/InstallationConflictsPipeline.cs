using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Sitecore.Remote.Installation.Client.Responses;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Core;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Installation conflicts pipeline 
  /// </summary>
  public class InstallationConflictsPipeline
  {
    /// <summary>
    /// Runs the specified package path.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="commands">The commands.</param>
    /// <returns></returns>
    public static async Task<NameValueCollection> Run(IHttpClient client, CommandsResponse commands)
    {
      var details = new InstallationConflictDetails(client, commands);

      await Pipeline.Run(PipelineNames.InstallationConflicts, details);

      var result = new NameValueCollection();

      if (!string.IsNullOrEmpty(details.Result))
      {
        result.Add("__RESULT", details.Result);
        result.Add("__PIPELINE", details.RemotePipelineId);
      }

      return result;
    }
  }
}
