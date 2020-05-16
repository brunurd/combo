using System;
using LavaLeak.Combo.Editor.Logging;
using UnityEngine;
using UnityEngine.UIElements;

namespace LavaLeak.Combo.Editor.Inspector.ComboConfig
{
    public class LogLevelComponent : VisualElement
    {
        private static Toggle Option(LogLevel value, Action<LogLevel> setValue, LogLevel defaultValue, string label)
        {
            var toggle = new Toggle {value = (value | defaultValue) == value, text = label};
            toggle.style.marginLeft = 10;

            toggle.RegisterCallback<ChangeEvent<bool>>(e =>
            {
                if (e.newValue)
                {
                    value ^= defaultValue;
                    setValue(value);
                }
                else
                {
                    value &= ~defaultValue;
                    setValue(value);
                }
            });

            return toggle;
        }

        internal LogLevelComponent(LogLevel value, Action<LogLevel> setValue)
        {
            style.display = DisplayStyle.Flex;
            style.flexDirection = FlexDirection.Row;

            var title = new Label("Log Level: ");
            title.style.unityFontStyleAndWeight = FontStyle.Bold;

            Add(title);
            Add(Option(value, setValue, LogLevel.Log, "info"));
            Add(Option(value, setValue, LogLevel.Warning, "warning"));
            Add(Option(value, setValue, LogLevel.Error, "error"));
        }
    }
}