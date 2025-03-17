using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerAttackState : EnemyState
{
    Enemy_DeathBringer enemy;

    public DeathBringerAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DeathBringer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttack=Time.time;
    }
    public override void Uddate()
    {
        base.Uddate();
        enemy.ZeroVelocity();
        if(triggerCalled){
            if(enemy.CanTeleport())
                stateMachine.ChangeState(enemy.teleportState);
            else 
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}