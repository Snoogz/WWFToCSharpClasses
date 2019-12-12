using System.Activities;
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
    public static List<MethodCOBJ> MethodCalls { get; set; }

    public static List<PropertyCOBJ> Start(BaseWWF rootActivity)
    {
      GlobalProperties = new List<PropertyCOBJ>();
      MethodCalls = new List<MethodCOBJ>();
      RecursiveTraverse(rootActivity);

      return GlobalProperties;
    }

    private static void RecursiveTraverse(BaseWWF rootActivity)
    {
      // add any variables in current scope to global scope
      if (rootActivity.Variables != null && rootActivity.Variables.Any())
        AddGlobalPropertiesToClass(rootActivity.Variables);

      // evaluate the next activity and recurse
      if (rootActivity.Activities != null && rootActivity.Activities.Any())
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

      // add method call to class body
      if (rootActivity.Arguments != null && rootActivity.Arguments.Any())
      {
        AddMethodCallToClass(rootActivity);
      }
    }

    private static void AddGlobalPropertiesToClass(IEnumerable<Variable> variables)
    {
      foreach (var variable in variables)
      {
        if (!GlobalProperties.Contains(new PropertyCOBJ(variable.Name, variable.Type.FullName)))
        {
          GlobalProperties.Add(new PropertyCOBJ(variable.Name, variable.Type.FullName));
        }
      }
    }

    private static void AddMethodCallToClass(BaseWWF activity)
    {
      var arguments = activity.Arguments
        .Select(argument => new PropertyCOBJ {Name = argument.Value, Position = argument.Position}).ToList();

      MethodCalls.Add(new MethodCOBJ
      {
        Name = activity.DisplayName,
        Arguments = arguments
      });
    }
  }
}