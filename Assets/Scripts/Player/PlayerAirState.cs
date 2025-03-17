using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
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
        if(player.IsWallDetected()) stateMachine.ChangeState(player.playerWallSliderState);
        if(player.IsGroundDetected()) stateMachine.ChangeState(player.playerIdleState);
        if(xInput!=0) player.SetRigidbody(xInput*player.moveSpeed*.8f,rb.velocity.y);
    }
}
