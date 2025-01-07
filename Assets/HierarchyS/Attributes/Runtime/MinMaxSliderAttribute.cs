using UnityEngine;
namespace  Shadowprofile.Attributes.HierarchyS
{
public class MinMaxSliderAttribute : PropertyAttribute
{
    public float MinLimit;
    public float MaxLimit;

    public MinMaxSliderAttribute(float minLimit, float maxLimit)
    {
        MinLimit = minLimit;
        MaxLimit = maxLimit;
    }
}
}