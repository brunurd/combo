using LavaLeak.Combo.Editor.Config;
using LavaLeak.Combo.Editor.UI.Inspectors;
using UnityEditor;
using UnityEngine;

namespace LavaLeak.Combo.Editor.UI.Windows
{
    public class ComboConfigWindow : EditorWindow
    {
        private Vector2 _scrollPos;

        [MenuItem("Tools/Combo Config", false, 1)]
        public static void Init()
        {
            var window = (ComboConfigWindow) GetWindow(typeof(ComboConfigWindow), false, "Combo");
            window.minSize = new Vector2(300, 400);
            window.Show();
        }

        private void OnGUI()
        {
            var target = ComboConfig.Instance;
            var serializedObject = new SerializedObject(target);

            serializedObject.Update();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            ComboConfigEditor.Render(serializedObject, target);

            EditorGUILayout.EndScrollView();

            serializedObject.ApplyModifiedProperties();
        }
    }
}