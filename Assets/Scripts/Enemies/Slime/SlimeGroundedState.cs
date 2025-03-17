using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundedState : EnemyState
{
    protected Enemy_Slime enemy;
    private Transform player;

    public SlimeGroundedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Slime _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }
    public override void Enter()
    {
        base.Enter();
        player=PlayerManage.instance.player.transform;

    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();
        if(enemy.IsPlayerDetected()||Vector2.Distance(enemy.transform.position,player.position)<=enemy.agroDistance)
            stateMachine.ChangeState(enemy.battleState);
    }
}
