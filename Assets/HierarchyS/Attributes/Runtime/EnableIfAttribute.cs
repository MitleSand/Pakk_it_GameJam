using UnityEngine;

namespace Shadowprofile.Attributes.HierarchyS
{
    public class EnableIfAttribute : PropertyAttribute
    {
        public string Condition;
        public EnableIfAttribute(string condition) => Condition = condition;
    }
}