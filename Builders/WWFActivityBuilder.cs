using System.Activities;
using System.Collections.Generic;
using System.Linq;
using WWFToCSharpClasses.Converters;
using WWFToCSharpClasses.Transfer;
using WWFToCSharpClasses.TraversalObjects;

namespace WWFToCSharpClasses.Builders
{
  public static class WWFActivityBuilder
  {
    private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private static TransferObject TransferObject { get; set; }

    public static TransferObject Start(ActivityBuilder activityBuilder)
    {
      TransferObject = new TransferObject();
      var rootActivity = activityBuilder.Implementation;

      _logger.Trace("Converting root activity to traversal object.");
      var traversalObject = FromGenericWWFObject.ToBaseTraversalObject(rootActivity);

      TraverseActivities(traversalObject, 0);

      return TransferObject;
    }

    private static void TraverseActivities(BaseTraversalObject activity, int level)
    {
      _logger.Trace($"Converting {activity.DisplayName} to traversal object.");

      _logger.Trace($"In Level: {level}.");
      AddActivityToTransferObject(activity, level);

      // add any variables in current scope to transfer object (as global scope variable)
      if (activity.Variables.Any())
      {
        _logger.Trace($"Has {activity.Variables.Count()} variables.");
        AddVariablesToTransferObject(activity.Variables);
      }

      // evaluate any activities in scope
      _logger.Trace($"Has {activity.Activities.Count()} child activities.");
      foreach (var childActivity in activity.Activities)
      {
        // convert child to traversal object
        var newActivity = FromGenericWWFObject.ToBaseTraversalObject(childActivity);

        // traverse child
        if (newActivity.IsSequence || newActivity.IsForEachActivity || newActivity.IsIfActivity ||
            newActivity.IsTryCatchActivity)
        {
          TraverseActivities(newActivity, level + 1);
        }
        else
        {
          AddActivityToTransferObject(newActivity, level);
        }
      }

      // evaluate if then 
      if (activity.Then != null)
      {
        _logger.Trace("In Then.");
        var thenActivity = FromGenericWWFObject.ToBaseTraversalObject(activity.Then);
        TraverseActivities(thenActivity, level + 1);
      }

      // evaluate if else
      if (activity.Else != null)
      {
        _logger.Trace("In Else");
        var elseActivity = FromGenericWWFObject.ToBaseTraversalObject(activity.Else);
        TraverseActivities(elseActivity, level + 1);
      }
    }

    private static void AddVariablesToTransferObject(IEnumerable<Variable> variables)
    {
      foreach (var variable in variables)
      {
        if (!TransferObject.GlobalProperties.Contains(new TransferPropertyObject { Name = variable.Name, Type = variable.Type.FullName }))
        {
          TransferObject.GlobalProperties.Add(new TransferPropertyObject { Name = variable.Name, Type = variable.Type.FullName });
        }

        if (!TransferObject.AssemblyReferences.Contains(variable.Type.AssemblyQualifiedName))
        {
          TransferObject.AssemblyReferences.Add(variable.Type.AssemblyQualifiedName);
        }
      }
    }

    private static void AddActivityToTransferObject(BaseTraversalObject activity, int level)
    {
      if (activity.IsSequence)
      {
        TransferObject.Activities.Add((level, new TransferSequenceObject
        {
          DisplayName = activity.DisplayName
        }));
      }

      if (activity.IsIfActivity)
      {
        TransferObject.Activities.Add((level, new TransferIfObject
        {
          DisplayName = activity.DisplayName,
          Condition = activity.Condition
        }));
      }

      if (activity.IsForEachActivity)
      {
        TransferObject.Activities.Add((level, new TransferForEachObject
        {
          DisplayName = activity.DisplayName,
          Values = activity.Values,
          Argument = activity.Argument
        }));
      }

      if (activity.IsTryCatchActivity)
      {
        TransferObject.Activities.Add((level, new TransferTryCatchObject
        {
          DisplayName = activity.DisplayName
        }));
      }

      if (activity.IsCodeActivity)
      {
        TransferObject.Activities.Add((level, new TransferCodeActivity
        {
          DisplayName = activity.DisplayName,
          Arguments = activity.Arguments.Select(a => a.Value).ToList()
        }));
      }
    }
  }
}