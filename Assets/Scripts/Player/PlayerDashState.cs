using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.charaterStats.MakeInvinsable(true);
        stateTimer=player.dashDuration;
        
        player.skill.dash.CreateCloneStart();
    }

    public override void Exit()
    {
        base.Exit();
        player.skill.dash.CreateCloneOver();
        player.SetRigidbody(0,rb.velocity.y);
        player.charaterStats.MakeInvinsable(false);
    }

    public override void Uddate()
    {
        base.Uddate();
        player.SetRigidbody(player.dashSpeed*player.dashDir,0f);
        if(player.IsWallDetected()) stateMachine.ChangeState(player.playerWallSliderState);
        if(stateTimer<=0) stateMachine.ChangeState(player.playerIdleState);
        player.fX.CreateAfterImage();
    }
}
