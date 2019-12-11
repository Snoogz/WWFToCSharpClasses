using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace WWFToCSharpClasses.WWFObjects
{
  public class TryCatchWWF : BaseWWF
  {
    public IEnumerable<Catch> Catches { get; set; }
    public Activity Finally { get; set; }
  }
}