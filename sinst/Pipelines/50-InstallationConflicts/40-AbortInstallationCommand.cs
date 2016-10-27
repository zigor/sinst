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
  [Processor(40)]
  public class AbortInstallationCommand : InstallerCommandProcessor
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AbortInstallationCommand"/> class.
    /// </summary>
    public AbortInstallationCommand() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AbortInstallationCommand"/> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    public AbortInstallationCommand(UiInstaller installer) : base(installer)
    {
    }

    /// <summary>
    ///   Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Process(IPipelineContext<InstallationConflictDetails> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));

      var command = context.Model.CommandsResponse.FindAllByName("SetStyle").FirstOrDefault(c => c.Id == "AbortMessage");
      if (command?.Value != string.Empty)
      {
        return;
      }

      this.Installer.Events.RaiseFailure();

      var message = "Installation was aborted by user.";
      this.Installer.Events.RaiseOutputRequired(message, MessageLevel.Error);
    }
  }
}