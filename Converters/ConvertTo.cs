using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xaml;
using Tyler.Ofs.Web.Services;
using WWFToCSharpClasses.ClassObjects;
using WWFToCSharpClasses.WWFObjects;

namespace WWFToCSharpClasses.Converters
{
  public class ConvertTo
  {
    public static BaseWWF WWFObject<T>(T input)
    {
      switch (input)
      {
        case Sequence sequence:
          return new SequenceWWF()
          {
            DisplayName = sequence.DisplayName,
            Variables = sequence.Variables,
            Activities = sequence.Activities
          };
        case TryCatch tryCatch:
          return new TryCatchWWF()
          {
            DisplayName = tryCatch.DisplayName,
            Variables = tryCatch.Variables,
            Activities = new Collection<Activity> { tryCatch.Try },
            Catches = tryCatch.Catches,
            Finally = tryCatch.Finally
          };
        case CodeActivity codeActivity:
          // Grad properties from activity
          var propertyInfos = (from p in codeActivity.GetType().GetProperties()
            where !p.Name.Equals("DisplayName") && !p.Name.Equals("Id")
            select p.GetValue(codeActivity, null) as Argument).ToList();

          // Convert each one to readable object
          // then add to activity
          var arguments = new List<PropertyWWF>();
          foreach (var property in propertyInfos)
          {
            switch (property.Direction)
            {
              case ArgumentDirection.In:
              {
                arguments.Add(new PropertyWWF
                {
                  Value = property.Expression.GetType().GetProperty("Value")?.GetValue(property.Expression).ToString(),
                  Position = (int) property.Direction
                });
                break;
              }
              case ArgumentDirection.Out:
              {
                arguments.Add(new PropertyWWF
                {
                  Value = property.Expression.GetType().GetProperty("ExpressionText")?.GetValue(property.Expression).ToString(),
                  Position = (int)property.Direction
                });
                break;
              }
              case ArgumentDirection.InOut:
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
          }

          return new CodeActivityWWF
          {
            DisplayName = codeActivity.DisplayName,
            Arguments = arguments
          };
        default:
          return null;
      }
    }

    public static ActivityBuilder ActivityBuilder(string xamlPath)
    {
      var xamlReader = ActivityXamlServices.CreateBuilderReader(new XamlXmlReader(
        File.OpenRead(xamlPath)),new XamlSchemaContext(AppDomain.CurrentDomain.GetAssemblies()));

      if (!(XamlServices.Load(xamlReader) is ActivityBuilder activityBuilder))
        return null;

      return activityBuilder;
    }
  }
}