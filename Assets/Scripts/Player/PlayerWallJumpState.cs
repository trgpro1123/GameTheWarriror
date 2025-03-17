using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetRigidbody(5*-player.facingDir,player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();

    }
    public override void Uddate()
    {
        base.Uddate();
        if(rb.velocity.y<0) stateMachine.ChangeState(player.playerAirState); 
    }
}
