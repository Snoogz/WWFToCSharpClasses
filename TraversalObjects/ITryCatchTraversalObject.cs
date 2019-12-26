using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace WWFToCSharpClasses.TraversalObjects
{
  public interface ITryCatchTraversalObject
  {
    // TryCatch
    bool IsTryCatchActivity { get; set; }
    IEnumerable<Catch> Catches { get; set; }
    Activity Finally { get; set; }
  }
}