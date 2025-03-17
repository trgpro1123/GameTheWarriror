using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.DotsActive(true);
        
    }
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.Busy(0.15f));
    }
    public override void Uddate()
    {
        
        base.Uddate();
        player.ZeroVelocity();
        if(Input.GetKeyUp(KeyCode.Mouse1)) stateMachine.ChangeState(player.playerIdleState);

        Vector2 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(player.transform.position.x>mousePosition.x&&player.facingDir==1){
            player.Flip();

        }else if(player.transform.position.x<mousePosition.x&&player.facingDir==-1){
            player.Flip();
        }
    }
}
