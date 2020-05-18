using System.Text;
using UnityEngine;

namespace LavaLeak.Combo.EnumFromJsonComboTask.Editor
{
    internal struct EnumFile
    {
        private string _contents;

        public EnumFile(string name, JsonFile file)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"namespace {Application.productName.ToPascalCase()} {{");
            builder.AppendLine($"\tenum {name.ToPascalCase()} {{");
            builder.Append("\t\t");
            builder.AppendLine(string.Join(",\n\t\t", file.enums));
            builder.AppendLine("\t}");
            builder.AppendLine("}");
            _contents = builder.ToString();
        }

        public override string ToString()
        {
            return _contents;
        }
    }
}