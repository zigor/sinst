using System;
using System.Net;
using System.Threading.Tasks;
using Sitecore.Remote.Installation.Client;
using Sitecore.Remote.Installation.Models;
using Sitecore.Remote.Installation.Pipelines.Core;
using Sitecore.Remote.Installation.Pipelines.Metadata;

namespace Sitecore.Remote.Installation.Pipelines
{
  /// <summary>
  /// Install package pipeline
  /// </summary>
  public class InstallPackagePipeline
  {
    /// <summary>
    /// Runs the specified package path.
    /// </summary>
    /// <param name="packagePath">The package path.</param>
    /// <param name="host">The host.</param>
    /// <param name="credentials">The credentials.</param>
    /// <returns></returns>
    public static async Task Run(string packagePath, string host, NetworkCredential credentials)
    {
      IHttpClient client = new HttpClient(new Connection { Host = host, Credentials = credentials});

      if (Pipeline.Run(PipelineNames.Authentication, client).Result)
      {
        await Pipeline.Run(PipelineNames.InstallPackage, new InstallPackageDetails(packagePath, client));
      }
    }
  }
}
