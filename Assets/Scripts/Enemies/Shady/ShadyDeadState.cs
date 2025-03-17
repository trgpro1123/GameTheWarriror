using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyDeadState : EnemyState
{
    Enemy_Shady enemy;
    public ShadyDeadState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Shady _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        
        if(triggerCalled)
            enemy.SeftDestroy();
        
    }
}
