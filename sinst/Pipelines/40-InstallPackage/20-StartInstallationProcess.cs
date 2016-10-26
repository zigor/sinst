using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Client.Requests;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Start installation process
  /// </summary>
  [Pipeline(PipelineNames.InstallPackage), Processor(20)]
  public class StartInstallationProcess
  {
    /// <summary>
    /// Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<InstallPackageDetails> context)
    {
      new InstallationPackageWizardRequest(context.Model.Client)
          .Install(context.Model.Package);
    }
  }
}
