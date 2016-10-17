using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;
using Sitecore.Remote.Installation.Storage;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Resolve package
  /// </summary>
  [Pipeline(PipelineNames.ResolvePackagePath), Processor(10)]
  public class ResolvePackagePath
  {
    /// <summary>
    /// Processes the specified client.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<Package> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));
      Assert.ArgumentNotNull(context.Model, nameof(context.Model));

      var repository = new LocalPackagesRepository();
      context.Model.Path = repository.Save(context.Model.Path);
    }
  }
}
