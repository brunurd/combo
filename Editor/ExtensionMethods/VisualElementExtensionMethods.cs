using UnityEngine.UIElements;

namespace LavaLeak.Combo.Editor.ExtensionMethods
{
    public static class VisualElementExtensionMethods
    {
        public static void StylePadding(this VisualElement element, StyleLength all)
        {
            element.style.paddingTop = all;
            element.style.paddingRight = all;
            element.style.paddingBottom = all;
            element.style.paddingLeft = all;
        }

        public static void StylePadding(this VisualElement element, StyleLength h, StyleLength v)
        {
            element.style.paddingTop = h;
            element.style.paddingRight = v;
            element.style.paddingBottom = h;
            element.style.paddingLeft = v;
        }

        public static void StylePadding(this VisualElement element, StyleLength top, StyleLength v, StyleLength bottom)
        {
            element.style.paddingTop = top;
            element.style.paddingRight = v;
            element.style.paddingBottom = bottom;
            element.style.paddingLeft = v;
        }

        public static void StylePadding(this VisualElement element, StyleLength top, StyleLength right,
            StyleLength bottom, StyleLength left)
        {
            element.style.paddingTop = top;
            element.style.paddingRight = right;
            element.style.paddingBottom = bottom;
            element.style.paddingLeft = left;
        }

        public static void StyleBorder(this VisualElement element, StyleFloat width)
        {
            element.style.borderTopWidth = width;
            element.style.borderRightWidth = width;
            element.style.borderBottomWidth = width;
            element.style.borderLeftWidth = width;
        }

        public static void StyleBorder(this VisualElement element, StyleFloat width, StyleColor color)
        {
            element.style.borderTopWidth = width;
            element.style.borderRightWidth = width;
            element.style.borderBottomWidth = width;
            element.style.borderLeftWidth = width;
            element.style.borderTopColor = color;
            element.style.borderRightColor = color;
            element.style.borderBottomColor = color;
            element.style.borderLeftColor = color;
        }
    }
}