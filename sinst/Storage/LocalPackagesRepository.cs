using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Sitecore.Remote.Installation.Diagnostic;

namespace Sitecore.Remote.Installation.Storage
{
  /// <summary>
  /// Local packages repository
  /// </summary>
  public class LocalPackagesRepository
  {
    /// <summary>
    /// Saves the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <exception cref="ArgumentException">Invalid path specified.</exception>
    public string Save(string path)
    {
      Assert.ArgumentNotNullOrEmpty(path, nameof(path));

      var uri = new Uri(path);
      var packageName = HttpUtility.UrlDecode(uri.Segments.LastOrDefault());
      if (string.IsNullOrEmpty(uri.AbsoluteUri) || string.IsNullOrEmpty(packageName))
      {
        throw new ArgumentException("Invalid path specified.", nameof(path));
      }
      
      var temporaryPackageName = Path.Combine(Path.GetTempPath(), packageName);

      if (File.Exists(temporaryPackageName))
      {
        return temporaryPackageName;
      }
    
      new WebClient().DownloadFile(uri, temporaryPackageName);

      return temporaryPackageName;
    }
  }
}
