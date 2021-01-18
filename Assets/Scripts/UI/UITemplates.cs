using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public static class UITemplates
{
    private static readonly Dictionary<string, VisualTreeAsset> _templates = new Dictionary<string, VisualTreeAsset>();

    public static VisualTreeAsset GetTemplate(string name)
    {
        if (!_templates.ContainsKey(name))
        {
            _templates[name] = Resources.Load<VisualTreeAsset>("ui_templates/" + name);
        }

        return _templates[name];
    }
}