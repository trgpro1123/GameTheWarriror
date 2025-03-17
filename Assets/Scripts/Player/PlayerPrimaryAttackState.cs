using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public int comboCounter;
    private float lastTimeAttack;
    private float comboWindow=2f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput=0;
        if(comboCounter>2||Time.time>lastTimeAttack+comboWindow) comboCounter=0;
        player.animator.SetInteger("ComboCounter",comboCounter);
        float attackDir=player.facingDir;
        if(xInput!=0) attackDir=xInput;
        player.SetRigidbody(player.attackMovement[comboCounter].x*attackDir,player.attackMovement[comboCounter].y);
        stateTimer=.1f;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttack=Time.time;
        player.StartCoroutine(player.Busy(0.15f));
    }

    public override void Uddate()
    {
        base.Uddate();
        if(stateTimer<0) player.SetRigidbody(0,0);
        if(triggerCalled) stateMachine.ChangeState(player.playerIdleState);
    }
}
