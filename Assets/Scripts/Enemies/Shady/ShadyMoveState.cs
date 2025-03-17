using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyMoveState : ShadyGroundedState
{
    public ShadyMoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName, Enemy_Shady _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();
        enemy.SetRigidbody(enemy.moveSpeed*enemy.facingDir,enemy.rb.velocity.y);
        if(!enemy.IsGroundDetected() ||enemy.IsWallDetected()){
            enemy.ZeroVelocity();
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}