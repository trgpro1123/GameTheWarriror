using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Heal Effect",menuName ="Data/Item effect/Heal Effect")]
public class HealEffect : ItemEffect
{
    [Range(0f,1f)]
    [SerializeField] private float healPercent;
    public override void ExcectEffect(Transform _target)
    {
        PlayerStats playerStats=PlayerManage.instance.player.GetComponent<PlayerStats>();
        int healAmount=Mathf.FloorToInt(playerStats.GetMaxHealth()*healPercent);
        playerStats.IncreaseHealBy(healAmount);

    }
}
