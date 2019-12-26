using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WWFToCSharpClassesTests.Templates
{
  [TestClass]
  public class TemplatesTokenReplacementTest
  {
    [TestMethod]
    public void ArgumentTemplateTest()
    {
      // Get WWF object

      // Get list of tokens to replace
      var replacementTokens = new List<string>
      {
        "{argumenttype}",
        "{argumentname}"
      };

      // Load Template into 
      var templateStringBuilder = new StringBuilder(File.ReadAllText(@"..\..\Templates\ArgumentTemplate.txt"));

      // Call Token replacement
      foreach (var replacementToken in replacementTokens)
      {
        
      }
    }
  }
}
