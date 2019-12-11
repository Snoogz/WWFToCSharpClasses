using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
#if DEBUG
      args[0] = @"";
#endif
      if (!args.Any())
        return;

      var activityBuilder = ConvertTo.ActivityBuilder(args[0]);
      var rootActivity = ConvertTo.WWFObject(activityBuilder.Implementation);

      if (rootActivity == null)
        return;

      var result = ActivityTraversal.Start(rootActivity);

      var classObject = new BaseCOBJ
      {
        Name = activityBuilder.Name,
        Arguments = activityBuilder.Properties.Select(p => new PropertyCOBJ {Name = p.Name, Type = p.Type.FullName}),
        Properties = result
      };

      CClassBuilder.Start(classObject);
    }
  }
}
