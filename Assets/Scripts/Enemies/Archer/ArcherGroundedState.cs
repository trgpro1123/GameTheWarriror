using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGroundedState : EnemyState
{
    protected Enemy_Archer enemy;
    private Transform player;
    public ArcherGroundedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Archer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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