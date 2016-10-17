using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Sitecore.Remote.Installation.Attributes;

namespace Sitecore.Remote.Installation.Pipelines.Core
{
  /// <summary>
  /// Pipeline core
  /// </summary>
  public class Pipeline
  {
    /// <summary>
    /// The pipelines
    /// </summary>
    private static readonly IDictionary<string, IEnumerable<Processor>> Pipelines;

    /// <summary>
    /// Initializes the <see cref="Pipeline"/> class.
    /// </summary>
    static Pipeline()
    {
      Pipelines = typeof(Pipeline).Assembly.GetTypes()
        .Where(t => t.IsDefined(typeof(PipelineAttribute), false))
        .GroupBy(t => t.GetCustomAttribute<PipelineAttribute>().PiplineName, t => t)
        .ToDictionary(k => k.Key, v => v.Select(p => new Processor(p, p.GetCustomAttribute<ProcessorAttribute>())).OrderBy(p => p.ProcessorAttribute.SortOrder).AsEnumerable());
    }

    /// <summary>
    /// Runs the specified name.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <param name="name">The name.</param>
    /// <param name="model">The model.</param>
    /// <returns></returns>
    public static async Task Run<TModel>(string name, TModel model)
    {
      IEnumerable<Processor> processors = null;
      if (!Pipelines.TryGetValue(name, out processors))
      {
        throw new ArgumentException($"The pipeline with name {name} was not found", nameof(name));
      }

      var context = new PipelineContext<TModel>(model);

      await Task.Run(() => processors.TakeWhile(p => {
                                        p.Run(context);
                                        return !context.Aborted;
                                      }).ToList());
    }    
  }
}
