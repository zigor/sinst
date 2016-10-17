using System;
using Sitecore.Remote.Installation.Attributes;
using Sitecore.Remote.Installation.Models;

namespace Sitecore.Remote.Installation.Pipelines.Core
{
  /// <summary>
  ///   Processor
  /// </summary>
  public class Processor
  {
    /// <summary>
    ///   The type
    /// </summary>
    private readonly Type type;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Processor" /> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="processorAttribute">The processor attribute.</param>
    public Processor(Type type, ProcessorAttribute processorAttribute)
    {
      this.type = type;
      this.ProcessorAttribute = processorAttribute;
    }

    /// <summary>
    ///   The processor metadata
    /// </summary>
    /// <value>
    ///   The processor attribute.
    /// </value>
    public ProcessorAttribute ProcessorAttribute { get; }

    /// <summary>
    ///   Runs the specified arguments.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="pipelineContext">The pipelineContext.</param>
    public void Run<TModel>(IPipelineContext<TModel> pipelineContext)
    {
      var instance = Activator.CreateInstance(this.type);
      this.type.GetMethod(this.ProcessorAttribute.MethodName).Invoke(instance, new object[] {pipelineContext});
    }
  }
}