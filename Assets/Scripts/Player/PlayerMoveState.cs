using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
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
        player.SetRigidbody(xInput*player.moveSpeed,rb.velocity.y);
        if(player.IsWallDetected()) stateMachine.ChangeState(player.playerIdleState);
        if(xInput==0) stateMachine.ChangeState(player.playerIdleState); 

    }
}
