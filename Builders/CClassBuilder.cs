using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
      var templateText = File.ReadAllText(@"..\..\Templates\ClassBaseTemplate.txt");
      templateText = templateText.Replace(@"{namespace}", result.Namespace);
      templateText = templateText.Replace(@"{classname}", result.Name);
      templateText = templateText.Replace(@"{classarguments}",
        result.Arguments.Select(a => $"{a.Type} {a.Name}")
          .Aggregate("", (current, argument) => current + (argument + ", ")).Trim(' ', ','));

      // Add properties

      // Add method calls

      // Write to CS file
      File.WriteAllText($@"..\..\Results\{result.Name}.cs", templateText);
    }
  }
}