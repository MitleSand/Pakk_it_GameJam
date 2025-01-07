using UnityEngine;

namespace Shadowprofile.Attributes.HierarchyS
{
    public class DisableIfAttribute : PropertyAttribute
    {
        public string Condition;
        public DisableIfAttribute(string condition) => Condition = condition;
    }
}