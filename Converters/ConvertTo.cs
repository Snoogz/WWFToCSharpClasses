using System.Activities;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Collections.ObjectModel;
using System.IO;
using System.Xaml;
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
        default:
          return null;
      }
    }

    public static ActivityBuilder ActivityBuilder(string xamlPath)
    {
      var xamlReader = ActivityXamlServices.CreateBuilderReader(new XamlXmlReader(
        File.OpenRead(xamlPath)));

      if (!(XamlServices.Load(xamlReader) is ActivityBuilder activityBuilder))
        return null;

      return activityBuilder;
    }
  }
}