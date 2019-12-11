using System.Activities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WWFToCSharpClasses.ClassObjects;
using WWFToCSharpClasses.Converters;
using WWFToCSharpClasses.WWFObjects;

namespace WWFToCSharpClasses.Traversal
{
  public static class ActivityTraversal
  {
    public static List<PropertyCOBJ> GlobalProperties { get; set; }
    public static List<PropertyCOBJ> Start(BaseWWF rootActivity)
    {
      GlobalProperties = new List<PropertyCOBJ>();
      RecursiveTraverse(rootActivity);

      return GlobalProperties;
    }

    private static void RecursiveTraverse(BaseWWF rootActivity)
    {
      // add any variables in current scope to global scope
      if (rootActivity.Variables.Any())
        AddGlobalPropertiesToClass(rootActivity.Variables);

      // evaluate the next activity and recurse
      if (rootActivity.Activities.Any())
      {
        foreach (var childActivity in rootActivity.Activities)
        {
          // make parseable
          var newRootActivity = ConvertTo.WWFObject(childActivity);

          if (newRootActivity == null)
            return;

          // recurse
          RecursiveTraverse(newRootActivity);
        }
      }
    }

    private static void AddGlobalPropertiesToClass(IEnumerable<Variable> variables)
    {
      foreach (var variable in variables)
      {
        if (!GlobalProperties.Contains(new PropertyCOBJ{Name = variable.Name, Type = variable.Type.FullName}))
        {
          GlobalProperties.Add(new PropertyCOBJ { Name = variable.Name, Type = variable.Type.FullName });
        }
      }
    }
  }
}