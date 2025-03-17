using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBattleState : EnemyState
{
    Enemy_Archer enemy;
    Transform player;
    private int moveDir;
    public ArcherBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Archer _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
    {
        enemy=_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player=PlayerManage.instance.player.transform;
        if(player.GetComponent<PlayerStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.safeDistance)
            {
                if (CanJump()==true)
                {
                    enemy.stateMachine.ChangeState(enemy.jumpState);
                }
            }

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    enemy.stateMachine.ChangeState(enemy.attackState);
                }
            }

        }
        else{

            if (stateTimer < 0 || Vector2.Distance(enemy.transform.position, player.position) > 10)
                stateMachine.ChangeState(enemy.idleState);
        }

        BattleStateFlipControll();

    }

    private void BattleStateFlipControll()
    {
        if (player.transform.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();
        else if (player.transform.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
    }

    private bool CanAttack(){
        if(Time.time>=enemy.lastTimeAttack+enemy.attackCoolDown){
            enemy.lastTimeAttack=Time.time;
            enemy.attackCoolDown=Random.Range(enemy.minAttackCoolDown,enemy.maxAttackCoolDown);
            return true;
        }
        return false;
    }
    private bool CanJump(){
        if(enemy.GroundCheckBehind()==false||enemy.WallBehind()==false) return false;
        if(Time.time>=enemy.lasTimeJump + enemy.jumpCooldown){
            enemy.lasTimeJump=Time.time;
            return true;
        }
        return false;

    }
}

