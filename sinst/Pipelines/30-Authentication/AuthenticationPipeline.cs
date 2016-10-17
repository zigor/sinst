using System.Threading.Tasks;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Core;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  ///   Authentication pipeline
  /// </summary>
  public class AuthenticationPipeline
  {
    /// <summary>
    ///   Runs this instance.
    /// </summary>
    /// <param name="client">The client.</param>
    public static async Task Run(IHttpClient client)
    {
      await Pipeline.Run(PipelineNames.Authentication, client);
    }
  }
}