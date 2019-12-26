using System.Collections.Generic;

namespace WWFToCSharpClasses.ClassObjects
{
  public class MethodCOBJ
  {
    public string Name { get; set; }
    public string ReturnVariable { get; set; }
    public IEnumerable<PropertyCOBJ> Arguments { get; set; }
  }
}