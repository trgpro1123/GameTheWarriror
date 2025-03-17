using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherJumpState : EnemyState
{
    Enemy_Archer enemy;
    public ArcherJumpState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Archer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity=new Vector2(enemy.JumpForce.x * -enemy.facingDir,enemy.JumpForce.y);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();
        if(rb.velocity.y<0&&enemy.IsGroundDetected())
            stateMachine.ChangeState(enemy.idleState); 
    }
}
