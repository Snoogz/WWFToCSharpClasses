using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;
using System.Xaml;

namespace WWFToCSharpClasses.Converters
{
  public class FromXAML
  {
    public static ActivityBuilder ToActivityBuilder(string xamlPath)
    {
      var xamlReader = ActivityXamlServices.CreateBuilderReader(new XamlXmlReader(
        File.OpenRead(xamlPath)), new XamlSchemaContext(AppDomain.CurrentDomain.GetAssemblies()));

      if (!(XamlServices.Load(xamlReader) is ActivityBuilder activityBuilder))
        return null;

      return activityBuilder;
    }
  }
}