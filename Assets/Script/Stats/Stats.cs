using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] private int baseValue;

    public List<int> modifiers;
    public int GetValue()
    {
        int finalValue = baseValue;

        foreach (var modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }
    public void SetDefaultValue(int value)
    {
        baseValue = value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
    }
    
    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
    }
}
