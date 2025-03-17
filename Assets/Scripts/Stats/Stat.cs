using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    public List<int> modifiers=new List<int>();
    public int GetValue(){
        int finalValue=baseValue;
        foreach (int value in modifiers)
        {
            finalValue+=value;
        }
        return finalValue;
    }
    public void SetDefaultValue(int _value){
        baseValue=_value;
    }
    public void AddModifier(int _modifiers){
        modifiers.Add(_modifiers);
    }
    public void RemoveModifier(int _modifiers){
        if(modifiers.Count>0) modifiers.Remove(_modifiers);
    }
}
