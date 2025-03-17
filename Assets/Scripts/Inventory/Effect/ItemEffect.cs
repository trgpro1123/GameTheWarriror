using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemEffect : ScriptableObject
{
    [TextArea]
    public string itemEffectDescription;
    public virtual void ExcectEffect(Transform _target){
        Debug.Log("Effect is used!");
    }
}
