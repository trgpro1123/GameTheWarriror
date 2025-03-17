using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStunnedState : EnemyState
{
    Enemy_Slime enemy;
    public SlimeStunnedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Slime _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.fX.InvokeRepeating("RedColorBlink",0,.1f);
        stateTimer=enemy.stunDuration;
        rb.velocity=new Vector2(-enemy.facingDir*enemy.stunDirection.x,enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.charaterStats.MakeInvinsable(false);
    }
    public override void Uddate()
    {
        base.Uddate();
        if(rb.velocity.y<.1f && enemy.IsGroundDetected()){
            enemy.fX.Invoke("CanncelColorChange",0);
            //enemy.animator.SetTrigger("StunFold");
            enemy.charaterStats.MakeInvinsable(true);
        }
        enemy.ZeroVelocity();
        if(stateTimer<0) stateMachine.ChangeState(enemy.idleState);

    }
}
