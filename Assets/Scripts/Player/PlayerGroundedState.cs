using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
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
        if(Input.GetKeyDown(KeyCode.E)&&player.skill.blackhole.blackholeUnlocked){
            if(player.skill.blackhole.cooldownTimer>0){
                player.fX.CreatePopUpText("CoolDown");
                return;
            }
            stateMachine.ChangeState(player.playerBlackholeState);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)&&hasNoSword()&&player.skill.sword.swordUnlocked && player.skill.sword.CanUseSkill()) stateMachine.ChangeState(player.playerAimSwordState);
        if(Input.GetKey(KeyCode.Q)&&player.skill.parry.parryUnlocked && player.skill.parry.CanUseSkill()) stateMachine.ChangeState(player.playerCounterState);
        if(Input.GetKey(KeyCode.Mouse0)) stateMachine.ChangeState(player.playerPrimaryAttackState);
        if(!player.IsGroundDetected()) stateMachine.ChangeState(player.playerAirState); 
        if(Input.GetKeyDown(KeyCode.Space)&&player.IsGroundDetected()){
            stateMachine.ChangeState(player.playerJumpState);
        }
        
    }

    private bool hasNoSword(){
        if(!player.sword){
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
