using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player =>GetComponentInParent<Player>();

    private void AnimationTrigger(){
        player.AnimatorTrigger();
    }
    public void AttackTrigger(){
        AudioManager.instance.PlaySFX(2);
        Collider2D []colliders=Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);
        foreach(var hit in colliders){
            if(hit.GetComponent<Enemy>()!=null){
                EnemyStats enemyStats=hit.GetComponent<EnemyStats>();
                player.charaterStats.DoDamage(enemyStats);
                ItemData_Equipment dataEquiment= Inventory.instance.GetEquiment(EquipmentType.Weapon);
                if(dataEquiment!=null) dataEquiment.ItemEffect(enemyStats.transform);
            }
        }
    }
    public void ThrowSword(){
        SkillManager.instance.sword.CreateSword();
        player.skill.sword.cooldownTimer=player.skill.sword.cooldown;
    }

}
