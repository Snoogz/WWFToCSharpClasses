using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WWFToCSharpClasses.ClassObjects
{
  public class BaseCOBJ
  {
    public string Namespace { get; set; }
    public string Name { get; set; }
    public IEnumerable<PropertyCOBJ> Arguments { get; set; }
    public IEnumerable<PropertyCOBJ> Properties { get; set; }
    public IEnumerable<MethodCOBJ> Methods { get; set; }

    public BaseCOBJ(ActivityBuilder activityBuilder, List<PropertyCOBJ> properties)
    {
      Namespace = activityBuilder.Name;
      Name = Regex.Match(activityBuilder.Name, @"[^.]*$").Value;
      Arguments = activityBuilder.Properties.Select(p => new PropertyCOBJ {Name = p.Name, FullName = p.Type.FullName});
      Properties = properties;
    }
  }
}