using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    Enemy_Slime enemy;

    public SlimeAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Slime _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        if(triggerCalled) stateMachine.ChangeState(enemy.battleState);
    }
}
