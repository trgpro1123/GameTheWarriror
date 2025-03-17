using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName ="Buff Effect",menuName ="Data/Item effect/Buff Effect")]
public class Buff_Effect : ItemEffect
{
    [SerializeField] private StatType buffType;
    [SerializeField] private int amountEffect;
    [SerializeField] private float effectDuration;
 
    PlayerStats playerStats;

    public override void ExcectEffect(Transform _target)
    {
        playerStats=PlayerManage.instance.player.GetComponent<PlayerStats>();
        playerStats.IncreaseStatBy(amountEffect,effectDuration,playerStats.GetType(buffType));
    }
    

}
