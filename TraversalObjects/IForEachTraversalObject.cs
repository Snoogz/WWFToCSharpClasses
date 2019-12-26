using System.Activities;

namespace WWFToCSharpClasses.TraversalObjects
{
  public interface IForEachTraversalObject
  {
    // ForEach
    bool IsForEachActivity { get; set; }
    string Values { get; set; }
    string Argument { get; set; }
    Activity Body { get; set; }
  }
}