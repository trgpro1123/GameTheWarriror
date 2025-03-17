using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSliderState : PlayerState
{
    public PlayerWallSliderState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
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
        if(player.IsWallDetected()==false)
            stateMachine.ChangeState(player.playerAirState);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.playerWallJumpState);
            return;
        }
        if(xInput!=0&&player.facingDir!=xInput) stateMachine.ChangeState(player.playerIdleState);
        if(yInput<0) player.SetRigidbody(0f,rb.velocity.y);
        else player.SetRigidbody(0f,rb.velocity.y*.6f);
        if(player.IsGroundDetected()) stateMachine.ChangeState(player.playerIdleState);
    }
}
