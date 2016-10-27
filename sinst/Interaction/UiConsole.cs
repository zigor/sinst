using System;
using System.Threading;
using System.Threading.Tasks;
using Sitecore.Remote.Installation.Installer.Events;

namespace Sitecore.Remote.Installation.Interaction
{
  /// <summary>
  /// Ui console
  /// </summary>
  public class UiConsole
  {
    /// <summary>
    /// The show progress
    /// </summary>
    private bool showProgress = true;

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

      this.Events.Start += (s, a) => this.StartProgress();
      this.Events.Failure += (s, a) => this.StopProgress();
      this.Events.Success += (s, a) => this.StopProgress();
    }

    /// <summary>
    /// Starts the progress.
    /// </summary>
    public void StartProgress()
    {
      Task.Run(() => this.Progress());
    }

    /// <summary>
    /// Progresses this instance.
    /// </summary>
    private void Progress()
    {
      this.showProgress = true;
      ResetLine();
      var progressBar = 0;
      while (this.showProgress)
      {
        progressBar += 1;
        if (progressBar > 8)
        {
          ResetLine();

          progressBar = 0;
        }
        Console.Write(".");

        Thread.Sleep(1000);
      }
    }

    /// <summary>
    /// Resets the line.
    /// </summary>
    private static void ResetLine()
    {
      Console.SetCursorPosition(0, Console.CursorTop);
      Console.Write(new string(' ', Console.WindowWidth - 1));
      Console.SetCursorPosition(0, Console.CursorTop);
    }

    /// <summary>
    /// Stops the progress.
    /// </summary>
    public void StopProgress()
    {
      this.showProgress = false;
    }

    /// <summary>
    /// Called when output requred.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="OutputRequredEventArgs"/> instance containing the event data.</param>
    private void OnOutputRequred(object sender, OutputRequredEventArgs e)
    {
      ResetLine();
      Console.WriteLine(e.Message);
    }

    /// <summary>
    /// Called when input required.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="InputRequiredEventArgs"/> instance containing the event data.</param>
    /// <exception cref="ArgumentException">Wrong choice</exception>
    private void OnInputRequired(object sender, InputRequiredEventArgs e)
    {
      this.StopProgress();

      ResetLine();

      Console.WriteLine(e.Message);
      Console.WriteLine("Please choose one of the following options:");
      Console.WriteLine();

      for (int i = 0; i < e.Choices.Count; ++i)
      {
        Console.WriteLine($"\t[{i + 1}]. {e.Choices.AllKeys[i]}");
      }

      Console.Write("> ");
      var key = Console.ReadLine();
      var option = GetOptionNumber(key);

      if (option < 0 && option >= e.Choices.Count)
      {
        throw new ArgumentException("Wrong choice", nameof(option));
      }

      e.Result = e.Choices[e.Choices.AllKeys[option]];

      this.StartProgress();
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