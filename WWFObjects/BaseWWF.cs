using System.Activities;
using System.Collections.Generic;

namespace WWFToCSharpClasses.WWFObjects
{
  public class BaseWWF
  {
    public string DisplayName { get; set; }
    public IEnumerable<Variable> Variables { get; set; }
    public IEnumerable<Activity> Activities { get; set; }
  }
}