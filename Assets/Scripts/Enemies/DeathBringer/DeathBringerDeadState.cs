using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerDeadState : EnemyState
{
    Enemy_DeathBringer enemy;
    public DeathBringerDeadState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DeathBringer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.animator.SetBool(enemy.lastAnimBoolName,true);
        enemy.animator.speed=0;
        enemy.cd.enabled=false;
        stateTimer=0.2f;
        
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void Uddate()
    {
        base.Uddate();
        if(stateTimer>0)
            rb.velocity =new Vector2(0,4);
    }
}