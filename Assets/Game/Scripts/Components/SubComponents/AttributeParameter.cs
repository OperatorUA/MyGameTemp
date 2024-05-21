using UnityEngine;

[System.Serializable]
public class AttributeParameter
{
    public float baseValue;
    public float currentValue;

    public void Init()
    {
        currentValue = baseValue;
    }
}
