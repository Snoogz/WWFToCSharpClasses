using System.Collections.Generic;

namespace WWFToCSharpClasses.Transfer
{
  public class TransferObject
  {
    public List<string> AssemblyReferences { get; set; }
    public List<TransferPropertyObject> GlobalProperties { get; set; }
    public List<(int scope, object activity)> Activities { get; set; }

    public TransferObject()
    {
      AssemblyReferences = new List<string>();
      GlobalProperties = new List<TransferPropertyObject>();
      Activities = new List<(int scope, object activity)>();
    }
  }

  public class TransferPropertyObject
  {
    public string Name { get; set; }
    public string Type { get; set; }
  }

  public class TransferSequenceObject
  {
    public string DisplayName { get; set; }
  }

  public class TransferIfObject
  {
    public string DisplayName { get; set; }
    public string Condition { get; set; }
  }

  public class TransferForEachObject
  {
    public string DisplayName { get; set; }
    public string Values { get; set; }
    public string Argument { get; set; }
  }

  public class TransferTryCatchObject
  {
    public string DisplayName { get; set; }
  }

  public class TransferCodeActivity
  {
    public string DisplayName { get; set; }
    public List<string> Arguments { get; set; }
  }
}