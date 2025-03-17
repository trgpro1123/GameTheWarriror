using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Uddate()
    {
        base.Uddate();
        if(xInput==player.facingDir&&player.IsWallDetected()) return;
        if(xInput!=0&&!player.isBusy) stateMachine.ChangeState(player.playerMoveState);
        
    }
}
