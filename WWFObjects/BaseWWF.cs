using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace WWFToCSharpClasses.WWFObjects
{
  public class BaseWWF
  {
    // General
    public string DisplayName { get; set; }
    public IEnumerable<Variable> Variables { get; set; }
    public IEnumerable<Activity> Activities { get; set; }

    // TryCatch
    public IEnumerable<Catch> Catches { get; set; }
    public Activity Finally { get; set; }

    // CodeActivity
    public IEnumerable<PropertyWWF> Arguments { get; set; }

    public BaseWWF()
    {
      DisplayName = "";
      Variables = new List<Variable>();
      Activities = new List<Activity>();
      Catches = new List<Catch>();
      Finally = null;
      Arguments = new List<PropertyWWF>();
    }
  }
}