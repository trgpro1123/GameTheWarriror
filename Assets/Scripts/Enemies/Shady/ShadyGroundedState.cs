using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyGroundedState : EnemyState
{
    private Transform player;
    protected Enemy_Shady enemy;
    public ShadyGroundedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Shady _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        if(enemy.IsPlayerDetected()||Vector2.Distance(enemy.transform.position,player.position)<=enemy.agroDistance){

            stateMachine.ChangeState(enemy.battleState);
        }
    }
}