using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WWFToCSharpClasses.TraversalObjects;

namespace WWFToCSharpClasses.Converters
{
  public static class FromGenericWWFObject
  {
    private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    public static BaseTraversalObject ToBaseTraversalObject<T>(T input)
    {
      var type = input.GetType();

      while (true)
      {
        if (type == null)
        {
          return null;
        }

        if (type == typeof(NativeActivity))
        {
          return ToActivityTraversalObject(input);
        }

        if (type == typeof(CodeActivity))
        {
          return ToCodeActivityTraversalObject(input);
        }

        if (type == typeof(ActivityWithResult))
        {
          return ToCodeActivityTraversalObject(input);
        }

        type = type.BaseType;
      }
    }

    private static BaseTraversalObject ToActivityTraversalObject<T>(T nativeActivity)
    {
      switch (nativeActivity)
      {
        case Sequence sequence:
          _logger.Trace("Match to Sequence.");
          return new BaseTraversalObject
          {
            DisplayName = sequence.DisplayName,
            Variables = sequence.Variables,
            Activities = sequence.Activities,
            IsSequence = true
          };
        case TryCatch tryCatch:
          _logger.Trace("Match to TryCatch.");
          return new BaseTraversalObject
          {
            DisplayName = tryCatch.DisplayName,
            IsTryCatchActivity = true,
            Variables = tryCatch.Variables,
            Activities = new Collection<Activity> { tryCatch.Try },
            Catches = tryCatch.Catches,
            Finally = tryCatch.Finally
          };
        case If @if:
          _logger.Trace("Match to If.");
          var props = @if.GetType().GetProperties();
          return new BaseTraversalObject
          {
            DisplayName = @if.DisplayName,
            IsIfActivity = true,
            Condition = @if.Condition.Expression.GetType().GetProperty("ExpressionText").GetValue(@if.Condition.Expression).ToString(),
            Then = @if.Then,
            Else = @if.Else
          };
        case NativeActivity other:
          if (other.GetType().FullName.Contains("ForEach"))
          {
            _logger.Trace("Match to ForEach.");
            return ToForEachTraversalObject(other);
          }
          return null;
        default:
          _logger.Warn("No match found.");
          return null;
      }
    }

    private static BaseTraversalObject ToForEachTraversalObject<T>(T forEach)
    {
      var values = forEach.GetType().GetProperty("Values")?.GetValue(forEach) as InArgument;
      var valueName = values?.Expression.GetType().GetProperty("ExpressionText")
        ?.GetValue(values.Expression).ToString();

      var body = forEach.GetType().GetProperty("Body")?.GetValue(forEach) as ActivityDelegate;
      var argument = body?.GetType().GetProperty("Argument")?.GetValue(body) as DelegateArgument;

      var displayName = forEach.GetType().GetProperty("DisplayName")?.GetValue(forEach).ToString();

      return new BaseTraversalObject
      {
        DisplayName = displayName,
        IsForEachActivity = true,
        Values = valueName,
        Argument = argument?.Name,
        Activities = new List<Activity> { body?.Handler }
      };
    }

    private static BaseTraversalObject ToCodeActivityTraversalObject<T>(T codeActivity)
    {
      _logger.Trace("Match to CodeActivity.");

      // Get properties from activity
      var propertyInfos = (from p in codeActivity.GetType().GetProperties()
        where !p.Name.Equals("DisplayName") && !p.Name.Equals("Id")
        select p.GetValue(codeActivity, null) as Argument).ToList();
      var displayName = codeActivity.GetType().GetProperty("DisplayName")?.GetValue(codeActivity).ToString();

      // Convert each one to readable object
      // then add to activity as argument
      var arguments = new List<PropertyTraversalObject>();
      foreach (var property in propertyInfos)
      {
        var newProperty = new PropertyTraversalObject();
        switch (property?.Direction)
        {
          case ArgumentDirection.In:
          {

            newProperty.Value = property.Expression.GetType().GetProperty("Value")?.GetValue(property.Expression)
              .ToString();
            break;
          }
          case ArgumentDirection.Out:
          {
            newProperty.Value = property.Expression.GetType().GetProperty("ExpressionText")
              ?.GetValue(property.Expression).ToString();
            break;
          }
          case ArgumentDirection.InOut:
            break;
          default:
            continue;
        }

        if (!arguments.Contains(newProperty))
          arguments.Add(newProperty);
      }

      return new BaseTraversalObject
      {
        DisplayName = displayName,
        Arguments = arguments,
        IsCodeActivity = true
      };
    }
  }
}