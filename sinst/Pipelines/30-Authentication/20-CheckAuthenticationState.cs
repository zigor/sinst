using System.Linq;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Check authentication state
  /// </summary>
  /// <seealso cref="Sitecore.Remote.Installation.Models.IHandler" />
  [Pipeline(PipelineNames.Authentication), Processor(20)]
  public class CheckAuthenticationState
  {
    /// <summary>
    /// Processes the specified client.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<IHttpClient> context)
    {
      var client = context.Model;

      if (client.Headers.AllKeys.All(k => k != "Cookie") ||
          !client.Headers["Cookie"].Contains(".ASPXAUTH"))
      {
        context.Aborted = true;
      }
    }
  }
}
