using System;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Diagnostic;
using Sitecore.Remote.Installation.Installer;
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
    public UiInstaller Installer { get; set; }

    public ResolvePackagePath() : this(UiInstaller.Instance)
    {
    }

    /// <summary>
    /// Initializes a new installer of the <see cref="ResolvePackagePath" /> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    private ResolvePackagePath(UiInstaller installer)
    {
      this.Installer = installer;
    }

    /// <summary>
    /// Processes the specified client.
    /// </summary>
    /// <param name="context">The context.</param>
    public void Process(IPipelineContext<Package> context)
    {
      Assert.ArgumentNotNull(context, nameof(context));
      Assert.ArgumentNotNull(context.Model, nameof(context.Model));

      var repository = new LocalPackagesRepository();

      try
      {
        context.Model.Path = repository.Save(context.Model.Path);
      }
      catch (ArgumentException ex)
      {
        this.Installer.Events.RaiseOutputRequired(ex.Message);
        this.Installer.Events.RaiseFailure();

        context.Aborted = true;
      }      
    }
  }
}
