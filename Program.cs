using System.Linq;
using WWFToCSharpClasses.Builders;
using WWFToCSharpClasses.Converters;

namespace WWFToCSharpClasses
{
  class Program
  {
    private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
      _logger.Trace("Starting...");

      if (!args.Any())
        return;

      _logger.Trace("Converting XAML to ActivityBuilder object.");
      var activityBuilder = FromXAML.ToActivityBuilder(args[0]);

      if (activityBuilder == null)
      {
        _logger.Warn("ActivityBuilder is null.");
        return;
      }

      _logger.Trace("Starting traversal.");
      var result = WWFActivityBuilder.Start(activityBuilder);

      //var classObject = new BaseCOBJ(activityBuilder, result);

      //CClassBuilder.Start(classObject);

      _logger.Trace("Shutting down.");
      NLog.LogManager.Shutdown();
    }
  }
}
