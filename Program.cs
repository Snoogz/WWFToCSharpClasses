using System.Linq;
using WWFToCSharpClasses.Builders;
using WWFToCSharpClasses.ClassObjects;
using WWFToCSharpClasses.Converters;
using WWFToCSharpClasses.Traversal;

namespace WWFToCSharpClasses
{
  class Program
  {
    static void Main(string[] args)
    {
      if (!args.Any())
        return;

      var activityBuilder = ConvertTo.ActivityBuilder(args[0]);
      var rootActivity = ConvertTo.WWFObject(activityBuilder.Implementation);

      if (rootActivity == null)
        return;

      var result = ActivityTraversal.Start(rootActivity);

      var classObject = new BaseCOBJ(activityBuilder, result);

      CClassBuilder.Start(classObject);
    }
  }
}
