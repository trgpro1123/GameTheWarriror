using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerTeleportState : EnemyState
{
    Enemy_DeathBringer enemy;
    public DeathBringerTeleportState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DeathBringer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.charaterStats.MakeInvinsable(true);
        // stateTimer=1;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.charaterStats.MakeInvinsable(false);
    }
    public override void Uddate()
    {
        base.Uddate();
        if(triggerCalled){
            if(enemy.CanCastSpell()){
                stateMachine.ChangeState(enemy.spellCastState);
            }
            else{
                stateMachine.ChangeState(enemy.battleState);
            }
        }
            // if(stateTimer<0)
            //     stateMachine.ChangeState(enemy.battleState);
    }
}

