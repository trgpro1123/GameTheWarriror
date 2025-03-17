using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherGroundedState
{
    public ArcherIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_Archer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer=enemy.idleTime;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();
        if(stateTimer<0) stateMachine.ChangeState(enemy.moveState);
    }
}
