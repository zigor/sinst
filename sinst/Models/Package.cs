namespace Sitecore.Remote.Installation.Models
{
  /// <summary>
  /// Package
  /// </summary>
  public class Package
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Package"/> class.
    /// </summary>
    /// <param name="path">The path.</param>
    public Package(string path)
    {
      this.Path = path;
    }

    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    public string Path { get; set; }
  }
}
