using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerMoveState : EnemyState
{
    Enemy_DeathBringer enemy;
    public DeathBringerMoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DeathBringer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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