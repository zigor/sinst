using System.Linq;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Installer.Events;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  ///   Abort installation command
  /// </summary>
  [Pipeline(PipelineNames.InstallationConflicts)]
  [Processor(60)]
  public class SuccessInstallationCommand : InstallerCommandProcessor
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="SuccessInstallationCommand" /> class.
    /// </summary>
    public SuccessInstallationCommand() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="SuccessInstallationCommand" /> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    public SuccessInstallationCommand(UiInstaller installer) : base(installer)
    {
    }

    /// <summary>
    ///   Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Process(IPipelineContext<InstallationConflictDetails> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));

      var command =
        context.Model.CommandsResponse.FindAllByName("SetStyle").FirstOrDefault(c => c.Id == "SuccessMessage");
      if (command?.Value != string.Empty)
      {
        return;
      }

      this.Installer.Events.RaiseOutputRequired("The package has been installed.", MessageLevel.Details);
    }
  }
}