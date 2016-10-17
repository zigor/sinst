using System;
using System.Collections;

namespace Sitecore.Remote.Installation.Attributes
{
  /// <summary>
  /// Pipelines attribute
  /// </summary>
  /// <seealso cref="System.Attribute" />
  public class PipelineAttribute : Attribute
  {
    public PipelineAttribute(string piplineName)
    {
      this.PiplineName = piplineName;
    }

    /// <summary>
    /// Gets or sets the name of the pipline.
    /// </summary>
    /// <value>
    /// The name of the pipline.
    /// </value>
    public string PiplineName { get; private set; }
  }
}
