using System.Collections;
using System.Collections.Generic;

namespace WWFToCSharpClasses.ClassObjects
{
  public class MethodCOBJ
  {
    public string Name { get; set; }
    public string ReturnType { get; set; }
    public IEnumerable<PropertyCOBJ> Arguments { get; set; }
  }
}