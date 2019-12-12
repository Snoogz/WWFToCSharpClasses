using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using WWFToCSharpClasses.ClassObjects;

namespace WWFToCSharpClasses.Builders
{
  public static class CClassBuilder
  {
    public static StringBuilder Builder { get; set; }

    public static void Start(BaseCOBJ result)
    {
      Builder = new StringBuilder();

      // Create base
      // Do token replacement
      var classTemplateText = File.ReadAllText(@"..\..\Templates\ClassBaseTemplate.txt");
      classTemplateText = classTemplateText.Replace(@"{namespace}", result.Namespace);
      classTemplateText = classTemplateText.Replace(@"{classname}", result.Name);
      classTemplateText = classTemplateText.Replace(@"{classarguments}",
        result.Arguments.Select(a => $"{a.Type} {a.Name}")
          .Aggregate("", (current, argument) => current + (argument + ", ")).Trim(' ', ','));

      // Add properties
      // Add assemblies for properties
      var propertyTemplateText = File.ReadAllText(@"..\..\Templates\PropertyTemplate.txt");
      var propertiesText = new StringBuilder();
      var assemblyTemplateText = File.ReadAllText(@"..\..\Templates\AssemblyTemplate.txt");
      var assembliesText = new StringBuilder();

      foreach (var property in result.Properties)
      {
        propertiesText.AppendLine(propertyTemplateText.Replace(@"{propertytype}", property.Type)
          .Replace(@"{propertyname}", property.Name));

        assembliesText.AppendLine(assemblyTemplateText.Replace(@"{assemblyname}", property.Assembly));
      }

      classTemplateText = classTemplateText.Replace(@"{classproperties}", propertiesText.ToString());
      classTemplateText = classTemplateText.Replace(@"{classassemblies}", assembliesText.ToString());

      // Add method calls


      // Write to CS file
      File.WriteAllText($@"..\..\Results\{result.Name}.cs", classTemplateText);
    }
  }
}