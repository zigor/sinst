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

      var packageUri = "https://github.com/zigor/sitecore.assemblyBinding/releases/download/v1.0/Sitecore.AssemblyBinding-1.0.zip";
      var host = "http://sc82";

      var credentials = new NetworkCredential("admin", "b");
      
      var packagePath = ResolvePackagePathPipeline.Run(packageUri).Result;

      InstallPackagePipeline.Run(packagePath, host, credentials).Wait();
    }
  }
}
