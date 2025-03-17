using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity=new Vector2(rb.velocity.x,player.jumpForce);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Uddate()
    {
        base.Uddate();
        if(xInput!=0) player.SetRigidbody(xInput*player.moveSpeed*.8f,rb.velocity.y);
        if(rb.velocity.y<0) stateMachine.ChangeState(player.playerAirState); 
    }
}
