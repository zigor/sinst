using System.Net;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines;

namespace sinst
{
  public class Program
  {
    /// <summary>
    /// Mains the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public static void Main(string[] args)
    {

      var packageUri = "";
      var host = "";

      var credentials = new NetworkCredential("", "");
      
      var packagePath = ResolvePackagePathPipeline.Run(packageUri).Result;

      InstallPackagePipeline.Run(packagePath, host, credentials).Wait();
    }
  }
}
