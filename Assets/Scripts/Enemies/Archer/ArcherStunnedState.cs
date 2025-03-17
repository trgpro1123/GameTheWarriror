using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStunnedState : EnemyState
{
 
    Enemy_Archer enemy;

    public ArcherStunnedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Archer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        enemy.fX.Invoke("CanncelColorChange",0);
    }
    public override void Uddate()
    {
        base.Uddate();
        if(stateTimer<0) stateMachine.ChangeState(enemy.idleState);

    }
}