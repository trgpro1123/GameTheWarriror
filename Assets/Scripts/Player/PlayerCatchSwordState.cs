using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sword=player.sword.transform;
        if(player.transform.position.x>sword.position.x&&player.facingDir==1){
            player.Flip();

        }else if(player.transform.position.x<sword.position.x&&player.facingDir==-1){
            player.Flip();
        }
        rb.velocity=new Vector2(player.swordReturnImpact*-player.facingDir,rb.velocity.y);
    }
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));

    }

    public override void Uddate()
    {
        base.Uddate();
        if(triggerCalled) stateMachine.ChangeState(player.playerIdleState);
    }
}
