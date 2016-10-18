using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Uploads package
  /// </summary>
  [Pipeline(PipelineNames.InstallPackage), Processor(10)]
  public class UploadPackage
  {
    /// <summary>
    /// Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<InstallPackageDetails> context)
    {
      
    }
  }
}
