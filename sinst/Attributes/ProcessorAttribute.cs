using System;

namespace Sitecore.Remote.Installation.Attributes
{
  /// <summary>
  /// Processor attribute
  /// </summary>
  /// <seealso cref="System.Attribute" />
  [AttributeUsage(AttributeTargets.Class, Inherited = false)]
  public class ProcessorAttribute : Attribute
  {
    /// <summary>
    /// The method name
    /// </summary>
    private string methodName;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessorAttribute"/> class.
    /// </summary>
    /// <param name="sortOrder">The sort order.</param>
    public ProcessorAttribute(int sortOrder)
    {
      this.SortOrder = sortOrder;

      this.MethodName = "Process";
    }

    /// <summary>
    /// Gets or sets the sort order.
    /// </summary>
    /// <value>
    /// The sort order.
    /// </value>
    public int SortOrder { get; set; }

    /// <summary>
    /// Gets or sets the name of the method.
    /// </summary>
    /// <value>
    /// The name of the method.
    /// </value>
    public string MethodName
    {
      get
      {
        return this.methodName;
      }
      set
      {
        this.methodName = value ?? "Process";
      }
    }
  }
}
