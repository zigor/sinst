using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Interaction;

namespace Sitecore.Remote.Installation
{
  /// <summary>
  /// Main program
  /// </summary>
  public class Program
  {
    /// <summary>
    /// Mains the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {
      var uiConsole = new UiConsole();
      var arguments = new Arguments(uiConsole, args);

      if (arguments.Options == null || string.IsNullOrEmpty(arguments.Package))
      {
        return;
      }

      arguments.Options.Input += uiConsole.Events.Input;
      arguments.Options.Output += uiConsole.Events.Output;

      arguments.Options.Start += uiConsole.Events.Start;
      arguments.Options.Success += uiConsole.Events.Success;
      arguments.Options.Failure += uiConsole.Events.Failure;

      uiConsole.StartProgress();

      UiInstaller.Instance.Install(arguments.Package, arguments.Options);

      uiConsole.StopProgress();

      arguments.Options.Input -= uiConsole.Events.Input;
      arguments.Options.Output -= uiConsole.Events.Output;
    }    
  }
}
