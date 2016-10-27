using System;
using System.Linq;
using Sitecore.Remote.Installation.Extensions;
using Sitecore.Remote.Installation.Installer.Events;
using Sitecore.Remote.Installation.Pipelines;

namespace Sitecore.Remote.Installation.Installer
{
  /// <summary>
  ///   Ui installer
  /// </summary>
  public class UiInstaller
  {
    /// <summary>
    ///   Initializes the <see cref="UiInstaller" /> class.
    /// </summary>
    static UiInstaller()
    {
      Instance = new UiInstaller();
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="UiInstaller" /> class.
    /// </summary>
    public UiInstaller()
    {
      this.Events = new UiEvents();
    }

    /// <summary>
    ///   Gets the events.
    /// </summary>
    /// <value>
    ///   The events.
    /// </value>
    public UiEvents Events { get; }

    /// <summary>
    ///   Gets the current.
    /// </summary>
    /// <value>
    ///   The current.
    /// </value>
    public static UiInstaller Instance { get; }

    /// <summary>
    /// Installs the specified package.
    /// </summary>
    /// <param name="packageUri">The package URI.</param>
    /// <param name="options">The options.</param>
    public void Install(string packageUri, InstallerOptions options)
    {

      EventHandler<InputRequiredEventArgs> inputHandler =
     (sender, args) => this.OnInputRequired(sender, args, options);

      EventHandler<OutputRequredEventArgs> outputHandler =
        (sender, args) => this.OnOutputRequired(sender, args, options);

      this.Events.Input += inputHandler;
      this.Events.Output += outputHandler;
      this.Events.Start += options.Start;
      this.Events.Success += options.Success;
      this.Events.Failure += options.Failure;

      this.BeginInstallation(packageUri);
      
      var packagePath = ResolvePackagePathPipeline.Run(packageUri).Result;
     
      InstallPackagePipeline.Run(packagePath, options.Host, options.Credentials).Wait();

      this.EndInstallation();

      this.Events.Input -= inputHandler;
      this.Events.Output -= outputHandler;
      this.Events.Start -= options.Start;
      this.Events.Success -= options.Success;
      this.Events.Failure -= options.Failure;
    }

    /// <summary>
    /// Ends the installation.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void EndInstallation()
    {
      this.Events.RaiseOutputRequired(string.Empty, MessageLevel.Details);
    }

    /// <summary>
    /// Begins the installation.
    /// </summary>
    /// <param name="packagePath">The package path.</param>
    private void BeginInstallation(string packagePath)
    {
      this.Events.RaiseOutputRequired("Installation started: " + packagePath, MessageLevel.Details);
      this.Events.RaiseOutputRequired(string.Empty, MessageLevel.Details);
    }

    /// <summary>
    /// Called when output required.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="OutputRequredEventArgs"/> instance containing the event data.</param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnOutputRequired(object sender, OutputRequredEventArgs e, InstallerOptions options)
    {
      var handler = options.Output;
      handler?.Invoke(sender, e);
    }

    /// <summary>
    /// Called when input required.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="InputRequiredEventArgs"/> instance containing the event data.</param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnInputRequired(object sender, InputRequiredEventArgs e, InstallerOptions options)
    {
      if (options.Silent && !string.IsNullOrEmpty(options.SilentOptionsSet))
      {
        var defaultOption = options.SilentOptionsSet.Split(';').FirstOrDefault(v => e.Choices.ContainsValue(v));
        if (!string.IsNullOrEmpty(defaultOption))
        {
          e.Result = defaultOption;
          return;
        }
      }

      var handler = options.Input;
      handler?.Invoke(sender, e);
    } 
  }
}