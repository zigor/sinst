using System.Threading.Tasks;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Core;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Resolve package pipelines
  /// </summary>
  public class ResolvePackagePathPipeline
  {
    /// <summary>
    /// Runs the specified uriPath.
    /// </summary>
    /// <param name="uriPath">The uriPath.</param>
    /// <returns></returns>
    public static string Run(string uriPath)
    {
      var package = new Package(uriPath);

      if (Pipeline.Run(PipelineNames.ResolvePackagePath, package).Result)
      {
        return package.Path;
      }
      return null;
    }
  }
}
