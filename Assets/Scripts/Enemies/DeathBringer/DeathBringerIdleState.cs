using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerIdleState : EnemyState
{
    Enemy_DeathBringer enemy;
    Player player;
    
    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_DeathBringer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer=enemy.idleTime;
        player=PlayerManage.instance.player;
        
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();
        if(Vector2.Distance(player.transform.position,enemy.transform.position)<10)
            enemy.startBattle=true;
        // if(Input.GetKeyDown(KeyCode.V))
        //     stateMachine.ChangeState(enemy.teleportState);
        if(stateTimer<0 && enemy.startBattle) stateMachine.ChangeState(enemy.battleState);
    }
}
