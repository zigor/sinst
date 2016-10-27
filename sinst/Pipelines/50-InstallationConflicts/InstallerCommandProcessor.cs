using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Installer command processor
  /// </summary>
  public abstract class InstallerCommandProcessor
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallerCommandProcessor"/> class.
    /// </summary>
    /// <param name="installer">The installer.</param>
    protected InstallerCommandProcessor(UiInstaller installer)
    {
      this.Installer = installer;
    }

    /// <summary>
    /// Gets the installer.
    /// </summary>
    /// <value>
    /// The installer.
    /// </value>
    public UiInstaller Installer { get; }

    /// <summary>
    ///   Processes the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    public abstract void Process(IPipelineContext<InstallationConflictDetails> context);
  }
}