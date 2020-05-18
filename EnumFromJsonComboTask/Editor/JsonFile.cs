using System;

namespace LavaLeak.Combo.EnumFromJsonComboTask.Editor
{
    [Serializable]
    internal struct JsonFile
    {
        public string[] enums;

        public JsonFile(string[] enums = null)
        {
            this.enums = enums ?? new string[0];
        }
    }
}