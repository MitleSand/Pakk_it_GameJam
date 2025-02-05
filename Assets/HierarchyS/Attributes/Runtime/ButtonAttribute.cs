using System;
using UnityEngine;

namespace Shadowprofile.Attributes.HierarchyS
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string buttonLabel;

        public ButtonAttribute(string buttonLabel = null)
        {
            this.buttonLabel = buttonLabel;
        }
    }
}