using System;
using Sitecore.Remote.Installation.Installer.Events;

namespace Sitecore.Remote.Installation.Interaction
{
  /// <summary>
  /// Ui console
  /// </summary>
  public class UiConsole
  {
    /// <summary>
    /// The events
    /// </summary>
    public UiEvents Events;

    /// <summary>
    ///   Initializes a new instance of the <see cref="UiConsole" /> class.
    /// </summary>
    public UiConsole()
    {
      this.Events = new UiEvents();

      this.Events.Input += OnInputRequired;
      this.Events.Output += OnOutputRequred;
    }

    /// <summary>
    /// Called when output requred.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="OutputRequredEventArgs"/> instance containing the event data.</param>
    private static void OnOutputRequred(object sender, OutputRequredEventArgs e)
    {
      Console.WriteLine(e.Message);
    }

    /// <summary>
    /// Called when input required.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="InputRequiredEventArgs"/> instance containing the event data.</param>
    /// <exception cref="ArgumentException">Wrong choice</exception>
    private static void OnInputRequired(object sender, InputRequiredEventArgs e)
    {
      Console.WriteLine("Please choose one of the following options:");
      Console.WriteLine();

      for (int i = 0; i < e.Choices.Count; ++i)
      {
        Console.WriteLine($"\t[{i + 1}]. {e.Choices.AllKeys[i]}");
      }

      Console.Write("...> ");
      var key = Console.ReadLine();
      var option = GetOptionNumber(key);

      if (option < 0 && option >= e.Choices.Count)
      {
        throw new ArgumentException("Wrong choice", nameof(option));
      }

      e.Result = e.Choices[e.Choices.AllKeys[option]];
    }


    /// <summary>
    /// Gets the option number.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    private static int GetOptionNumber(string key)
    {
      int option = 0;
      if (int.TryParse(key, out option))
      {
        return option - 1;
      }
      return -1;
    }
  }
}