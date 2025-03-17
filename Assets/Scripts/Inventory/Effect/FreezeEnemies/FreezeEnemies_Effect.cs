using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Freeze Enemies Effect",menuName ="Data/Item effect/Freeze Enemies Effect")]
public class FreezeEnemies_Effect : ItemEffect
{
    [SerializeField] private float effectDuration;

    public override void ExcectEffect(Transform _target)
    {
        PlayerStats playerStats=PlayerManage.instance.player.GetComponent<PlayerStats>();
        //Debug.Log(playerStats.currentHealth);
        if(playerStats.currentHealth>playerStats.GetMaxHealth()*0.2) return;
        if(!Inventory.instance.CanUseArmo()) return;
        Collider2D []colliders=Physics2D.OverlapCircleAll(_target.position,2);
        foreach(var hit in colliders){
            hit.GetComponent<Enemy>()?.FreezeTimerFor(effectDuration);
        }
    }
}
