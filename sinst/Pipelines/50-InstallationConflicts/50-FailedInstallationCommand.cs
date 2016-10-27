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
  [Processor(50)]
  public class FailedInstallationCommand : InstallerCommandProcessor
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="FailedInstallationCommand" /> class.
    /// </summary>
    public FailedInstallationCommand() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="FailedInstallationCommand" /> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    public FailedInstallationCommand(UiInstaller installer) : base(installer)
    {
    }

    /// <summary>
    ///   Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Process(IPipelineContext<InstallationConflictDetails> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));

      var command = context.Model.CommandsResponse.FindAllByName("SetStyle").FirstOrDefault(c => c.Id == "ErrorMessage");
      if (command?.Value != string.Empty)
      {
        return;
      }

      var messages = context.Model.CommandsResponse.FindAllByName("SetInnerHtml").ToArray();

      var errorDescription = messages.FirstOrDefault(m => m.Id == "ErrorDescription")?.Value;
      var failureReason = messages.FirstOrDefault(m => m.Id == "FailingReason")?.Value;

      this.Installer.Events.RaiseFailure();

      this.Installer.Events.RaiseOutputRequired("The installation failed.", MessageLevel.Error);
      this.Installer.Events.RaiseOutputRequired(failureReason, MessageLevel.Error);
      this.Installer.Events.RaiseOutputRequired(errorDescription, MessageLevel.Error);
    }
  }
}