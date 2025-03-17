using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBattleState : EnemyState
{
    private Enemy_Slime enemy;
    private Transform player;
    private int moveDir;
    public SlimeBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animateBoolName,Enemy_Slime _enemy) : base(_enemyBase, _enemyStateMachine, _animateBoolName)
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
        if(enemy.IsPlayerDetected()){
            stateTimer=enemy.battleTime;
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance) CanAttack();
        }
        else
            if(stateTimer<0||Vector2.Distance(enemy.transform.position,player.position)>10) stateMachine.ChangeState(enemy.idleState);

        if(player.transform.position.x<enemy.transform.position.x)
            moveDir=-1;
        else if(player.transform.position.x>enemy.transform.position.x)
            moveDir=1;


        if(enemy.IsPlayerDetected()&&enemy.IsPlayerDetected().distance < enemy.attackDistance-.1f)
            return;

        enemy.SetRigidbody(enemy.moveSpeed*moveDir,rb.velocity.y);
    }
    private void CanAttack(){
        if(Time.time>=enemy.lastTimeAttack+enemy.attackCoolDown){
            enemy.lastTimeAttack=Time.time;
            enemy.attackCoolDown=Random.Range(enemy.minAttackCoolDown,enemy.maxAttackCoolDown);
            stateMachine.ChangeState(enemy.attackState);
        }
    }
}
