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
      this.Events = new UiInstallerEvents();
    }

    /// <summary>
    ///   Gets the events.
    /// </summary>
    /// <value>
    ///   The events.
    /// </value>
    public UiInstallerEvents Events { get; }

    /// <summary>
    ///   Gets the current.
    /// </summary>
    /// <value>
    ///   The current.
    /// </value>
    public static UiInstaller Instance { get; }
  }
}