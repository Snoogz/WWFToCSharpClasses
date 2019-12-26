using System.Activities;

namespace WWFToCSharpClasses.TraversalObjects
{
  public interface IIfTraversalObject
  {
    // If
    bool IsIfActivity { get; set; }
    string Condition { get; set; }
    Activity Then { get; set; }
    Activity Else { get; set; }
  }
}