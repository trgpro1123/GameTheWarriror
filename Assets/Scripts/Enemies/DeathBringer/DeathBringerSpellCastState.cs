using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerSpellCastState : EnemyState
{
    Enemy_DeathBringer enemy;
    private int amountCastSpell;
    private float spellTimer;
    public DeathBringerSpellCastState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DeathBringer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();
        spellTimer=enemy.cooldownSpell+.5f;
        amountCastSpell=enemy.numberCastSpell;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeCastSpell=Time.time;
    }
    public override void Uddate()
    {
        base.Uddate();
        spellTimer-=Time.deltaTime;
        if(CanCast()){
            enemy.CreateSpell();
        }
        if(amountCastSpell<=0){

            stateMachine.ChangeState(enemy.teleportState);
        }

        
    }
    private bool CanCast(){
        if(spellTimer<0 && amountCastSpell>0){
            spellTimer=enemy.cooldownSpell;
            amountCastSpell--;
            return true;
        }
        return false;
    }
}
