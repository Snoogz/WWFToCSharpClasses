using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WWFToCSharpClasses.Converters;

namespace WWFToCSharpClassesTests
{
  [TestClass]
  public class BasicWorkflowTests
  {
    [TestMethod]
    public void BasicWorkflowTest1()
    {
      var workflowStringBuilder = new StringBuilder(File.ReadAllText(@"..\..\Workflows\BasicWorkflow.xaml"));

      var activityBuilder =  FromXAML.ToActivityBuilder(workflowStringBuilder.ToString());

      if (activityBuilder != null)
        Assert.Fail("Activity builder is null.");
    }
  }
}
