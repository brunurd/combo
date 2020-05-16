using LavaLeak.Combo.Editor.Config;
using UnityEditor;
using UnityEngine.UIElements;

namespace LavaLeak.Combo.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ComboTaskConfig))]
    public class ComboTaskConfigDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var item = new Box();
            return item;
        }
    }
}