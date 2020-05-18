using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LavaLeak.Combo.EnumFromJsonComboTask.Editor
{
    internal struct EnumFile
    {
        private string _contents;

        public EnumFile(string name, JsonFile file)
        {
            var builder = new StringBuilder();
            var invalidRegex = new Regex(@"^[0-9]");

            builder.AppendLine($"namespace {Application.productName.ToPascalCase()} {{");
            builder.AppendLine($"\tpublic enum {name.ToPascalCase()} {{");

            var enums = new List<string>();

            foreach (var enumValue in file.enums)
            {
                var isInvalid = invalidRegex.Match(enumValue).Success;

                if (!isInvalid)
                {
                    enums.Add($"\t\t{enumValue.ToPascalCase()}");
                }
            }

            builder.AppendLine(string.Join(",\n", enums));

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