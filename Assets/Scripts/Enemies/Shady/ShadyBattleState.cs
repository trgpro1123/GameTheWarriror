using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyBattleState : EnemyState
{
    Enemy_Shady enemy;
    Transform player;
    private int moveDir;
    private float defaultMoveSpeed;
    public ShadyBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Shady _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        defaultMoveSpeed=enemy.moveSpeed;
        enemy.moveSpeed=enemy.moveFast;
        player=PlayerManage.instance.player.transform;
        if(player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.moveSpeed=defaultMoveSpeed;
    }
    public override void Uddate()
    {
        base.Uddate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;


            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                enemy.charaterStats.KillYourSelf();
            }

        }
        else{

            if (stateTimer < 0 || Vector2.Distance(enemy.transform.position, player.position) > 10)
                stateMachine.ChangeState(enemy.idleState);
        }
        
        enemy.SetRigidbody(enemy.moveSpeed*enemy.facingDir,rb.velocity.y);
        BattleStateFlipControll();

    }

    private void BattleStateFlipControll()
    {
        if (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
        else if (player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
    }

}