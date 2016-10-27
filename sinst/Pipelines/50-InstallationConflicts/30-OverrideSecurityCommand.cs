using System.Collections.Specialized;
using System.Web;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  ///   Override security command
  /// </summary>
  [Pipeline(PipelineNames.InstallationConflicts)]
  [Processor(30)]
  public class OverrideSecurityCommand : InstallerCommandProcessor
  {
    /// <summary>
    ///   The default choises
    /// </summary>
    private static readonly NameValueCollection defaultChoices;

    /// <summary>
    ///   Initializes the <see cref="OverrideSecurityCommand" /> class.
    /// </summary>
    static OverrideSecurityCommand()
    {
      defaultChoices = new NameValueCollection
      {
        {"Continue", "continue"},
        {"Always", "always"},
        {"Abort", "abort"}
      };
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="OverrideSecurityCommand" /> class.
    /// </summary>
    public OverrideSecurityCommand() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="OverrideSecurityCommand" /> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    public OverrideSecurityCommand(UiInstaller installer) : base(installer)
    {
    }

    /// <summary>
    ///   Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public override void Process(IPipelineContext<InstallationConflictDetails> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));

      var command = context.Model.CommandsResponse.FindByName("ShowModalDialog");
      if (command?.Value?.Contains("xmlcontrol=ContinueAlwaysAbort") != true)
      {
        return;
      }

      var message = HttpUtility.ParseQueryString(command.Value)["te"];
      var choice = this.Installer.Events.RaiseInputRequired(message, new NameValueCollection(defaultChoices));

      context.Model.Result = choice;
      context.Model.RemotePipelineId = context.Model.CommandsResponse.FindByName("SetPipeline")?.Value;
    }
  }
}