using System.Collections.Generic;

namespace WWFToCSharpClasses.TraversalObjects
{
  public interface ICodeActivityTraversalObject
  {
    // CodeActivity
    bool IsCodeActivity { get; set; }
    IEnumerable<PropertyTraversalObject> Arguments { get; set; }
  }
}