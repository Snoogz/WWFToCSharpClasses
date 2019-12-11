using System.Collections.Generic;

namespace WWFToCSharpClasses.ClassObjects
{
  public class BaseCOBJ
  {
    public string Namespace { get; set; }
    public string Name { get; set; }
    public IEnumerable<PropertyCOBJ> Arguments { get; set; }
    public IEnumerable<PropertyCOBJ> Properties { get; set; }
    public IEnumerable<MethodCOBJ> Methods { get; set; }
  }
}