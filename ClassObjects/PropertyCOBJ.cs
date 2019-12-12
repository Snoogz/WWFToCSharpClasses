using System;
using System.Text.RegularExpressions;

namespace WWFToCSharpClasses.ClassObjects
{
  public class PropertyCOBJ
  {
    public string Name { get; set; }

    private string _type;

    public string Type
    {
      get => _type;
      set
      {
        switch (value)
        {
          case "String":
          {
            _type = "string";
            break;
          }
          case "Int32":
          {
            _type = "int";
            break;
          }
          default:
          {
            _type = value;
            break;
          }
        }
      }
    }

    public string Assembly { get; set; }

    public string FullName
    {
      set
      {
        var fullMatch = Regex.Match(value, @"\[\[(.+?),");
        var match = Regex.Match(fullMatch.Groups[1].Value, @"(^.*)\.(?=([^.]*))");
        Assembly = match.Groups[1].Value;
        Type = match.Groups[2].Value;
      }
    }

    public int Position { get; set; }

    public PropertyCOBJ() { }

    public PropertyCOBJ(string name, string type)
    {
      Name = name;
      var match = Regex.Match(type, @"(^.*)\.(?=([^.]*))");
      Assembly = match.Groups[1].Value;
      Type = match.Groups[2].Value;
    }
  }
}