using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;

namespace WWFToCSharpClasses.TraversalObjects
{
  public class BaseTraversalObject : ITryCatchTraversalObject, IForEachTraversalObject, ICodeActivityTraversalObject, IIfTraversalObject
  {
    // General
    public string DisplayName { get; set; }
    public IEnumerable<Variable> Variables { get; set; }
    public IEnumerable<Activity> Activities { get; set; }
    public bool IsSequence { get; set; }

    public BaseTraversalObject()
    {
      DisplayName = "";
      Variables = new List<Variable>();
      Activities = new List<Activity>();
      Catches = new List<Catch>();
      Finally = null;
      Arguments = new List<PropertyTraversalObject>();
    }

    public bool IsTryCatchActivity { get; set; }
    public IEnumerable<Catch> Catches { get; set; }
    public Activity Finally { get; set; }
    public bool IsForEachActivity { get; set; }
    public string Values { get; set; }
    public string Argument { get; set; }
    public Activity Body { get; set; }
    public bool IsCodeActivity { get; set; }
    public IEnumerable<PropertyTraversalObject> Arguments { get; set; }
    public bool IsIfActivity { get; set; }
    public string Condition { get; set; }
    public Activity Then { get; set; }
    public Activity Else { get; set; }
  }
}