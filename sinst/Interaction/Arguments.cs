using System;
using System.Linq;
using System.Net;
using Sitecore.Remote.Installation.Installer;
using Sitecore.Remote.Installation.Installer.Events;

namespace Sitecore.Remote.Installation.Interaction
{
  public class Arguments
  {
    /// <summary>
    /// The short options set
    /// </summary>
    private static readonly string[] ShortOptionsSet = { "-?", "?", "-s", "-p", "-h", "-u", "-pw", "-silent", "-package", "-host", "-user", "-password" };

    /// <summary>
    /// The long options set
    /// </summary>
    private static readonly string[] LongOptionsSet = { "-help", "-silent", "-package", "-host", "-user", "-password" };

    /// <summary>
    /// The default user name
    /// </summary>
    private static readonly string defaultUserName = "admin";

    /// <summary>
    /// The default passwrod
    /// </summary>
    private static readonly string defaultPasswrod = "b";

    /// <summary>
    /// Gets the console.
    /// </summary>
    /// <value>
    /// The console.
    /// </value>
    public UiConsole Console { get; }

    /// <summary>
    /// Gets the package.
    /// </summary>
    /// <value>
    /// The package.
    /// </value>
    public string Package { get; }

    /// <summary>
    /// Gets the options.
    /// </summary>
    /// <value>
    /// The options.
    /// </value>
    public InstallerOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Arguments"/> class.
    /// </summary>
    /// <param name="console">The console.</param>
    /// <param name="args">The arguments.</param>
    public Arguments(UiConsole console, string[] args)
    {
      this.Console = console;
      if (this.ParseShowHelp(args))
      {
        this.ShowHelp();
      }
      else
      {
        this.Package = this.ParsePackageUri(args);
        this.Options = this.ParseInstallerOptions(args);

        if (this.Options == null || string.IsNullOrEmpty(this.Package))
        {
          this.ShowHelp();
        }
      }
    }
    
    /// <summary>
    /// Shows the help.
    /// </summary>
    private void ShowHelp()
    {
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired("sinst host package [user] [password]");
      this.Console.Events.RaiseOutputRequired("      -h host -p package [-u user] [-pw password] [-s]");
      this.Console.Events.RaiseOutputRequired("      -host host -package package [-user user] [-password password] [-silent]");
      this.Console.Events.RaiseOutputRequired(string.Empty);

      this.Console.Events.RaiseOutputRequired("Description:");
      this.Console.Events.RaiseOutputRequired("\tThis tool allows users to install a Sitecore package to a Sitecore site.");
      this.Console.Events.RaiseOutputRequired("\tThe package installation requires available Sitecore backend.");
      this.Console.Events.RaiseOutputRequired(string.Empty);

      this.Console.Events.RaiseOutputRequired("Parameter List:");
      this.Console.Events.RaiseOutputRequired("\t-h|-host\thost\t\tSitecore site to connect to.");
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired("\t-p|-package\tpackage\t\tSpecifies a package path.");
      this.Console.Events.RaiseOutputRequired("\t       \t\t       \t\tThe path can be either a file path or url");
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired("\t-u|-user\tuser\t\tSpecifies a username for Sitecore authentication.");
      this.Console.Events.RaiseOutputRequired("\t       \t\t       \t\tThe default value is \"admin\"");
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired("\t-pw|-password\tpassword\tSpecifies a password for Sitecore authentication.");
      this.Console.Events.RaiseOutputRequired("\t       \t \t      \t\tThe default value is \"b\"");
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired("\t-s|-silent\ttrue|false\tDisables prompts and resolve all items, files and security conflicts silently.");
      this.Console.Events.RaiseOutputRequired("\t       \t\t       \t\tThe default value is \"true\"");
      this.Console.Events.RaiseOutputRequired(string.Empty);
      this.Console.Events.RaiseOutputRequired("\t?|-?|-help\t\t\tDisplays this help message.");
    }

    /// <summary>
    /// Parses the show help.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns></returns>
    private bool ParseShowHelp(string[] args)
    {
      return args.Length == 0 || args.Any(a => a == "?" || a == "-?" || a == "-help");
    }

    /// <summary>
    /// Parses the package path.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns></returns>
    private string ParsePackageUri(string[] args)
    {
      var packageUri = FindOptionValue(args, 1, "-p", "-package");

      if (string.IsNullOrEmpty(packageUri))
      {
        this.Console.Events.RaiseOutputRequired("The package name was not spesified.", MessageLevel.Error);
        return null;
      }

      return packageUri;
    }

    /// <summary>
    /// Finds the option value.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <param name="defaultPositioninArgument">The default positionin argument.</param>
    /// <param name="optionNames">The option names.</param>
    /// <returns></returns>
    private static string FindOptionValue(string[] args, int defaultPositioninArgument, params string[] optionNames)
    {
      if (args.Any(ShortOptionsSet.Contains) || args.Any(LongOptionsSet.Contains))
      {
        var option = args.FirstOrDefault(optionNames.Contains);

        if (option != null)
        {
          return string.Join(string.Empty, args.SkipWhile(a => a != option).SkipWhile(a => a == option).TakeWhile(a => !ShortOptionsSet.Contains(a) && !LongOptionsSet.Contains(a))).Trim();
        }
        return null;
      }

      return args.Length >= 2 && defaultPositioninArgument < args.Length ? args[defaultPositioninArgument] : null;
    }

    /// <summary>
    /// Parses the installer options.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns></returns>
    private InstallerOptions ParseInstallerOptions(string[] args)
    {
      var host = FindOptionValue(args, 0, "-h", "-host");

      if (string.IsNullOrEmpty(host))
      {
        this.Console.Events.RaiseOutputRequired("The sitecore host was not spesified." + Environment.NewLine + "Use -h or -host parameter to specify Sitecore host name", MessageLevel.Error);
        return null;
      }

      var user = FindOptionValue(args, 2, "-u", "-user") ?? defaultUserName;
      var password = FindOptionValue(args, 2, "-pw", "-password") ?? defaultPasswrod;

      var credentials = new NetworkCredential(user, password);

      var options = new InstallerOptions(host, credentials);

      if (FindOptionValue(args, int.MaxValue, "-s", "-silent") == "false")
      {
        options.Silent = false;
      }
      return options;
    }
  }
}
