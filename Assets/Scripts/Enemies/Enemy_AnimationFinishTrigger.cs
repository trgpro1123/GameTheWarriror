using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationFinishTrigger : MonoBehaviour
{
    Enemy enemy=>GetComponentInParent<Enemy>();
    public void AnimationTrigger(){
        enemy.AnimatorTrigger();
    }
    public void AttackTrigger(){
        Collider2D []colliders=Physics2D.OverlapCircleAll(enemy.attackCheck.position,enemy.attackCheckRadius);
        foreach(var hit in colliders){
            if(hit.GetComponent<Player>()!=null){
                PlayerStats playerStats=hit.GetComponent<PlayerStats>();
                enemy.charaterStats.DoDamage(playerStats);
            }
        }
    }
    public void SpecialAttackTrigger(){
        enemy.AnimationSpecialAttackTrigger();
    }
    public void OpenCounterWindow()=>enemy.OpenCounterAttackWindown();
    public void CloseCounterWindown()=>enemy.CloseCounterAttackWindown();

}
