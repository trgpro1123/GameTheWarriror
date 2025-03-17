using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterState : PlayerState
{
    private bool canCreateClone=true;
    public PlayerCounterState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone=true;
        stateTimer=player.counterAttakDuration;
        player.animator.SetBool("SuccessfulCouterAttack",false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Uddate()
    {
        base.Uddate();
        player.ZeroVelocity();
        Collider2D []colliders=Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);
        foreach(var hit in colliders){

            if(hit.GetComponent<Arrow_Controller>()!=null){
                hit.GetComponent<Arrow_Controller>().FlipArrow();
                SuccesfulCouterAttack();
            }
            if(hit.GetComponent<Enemy>()!=null&&hit.GetComponent<Enemy>().CanBeStunned())
            {
                SuccesfulCouterAttack();
                player.skill.parry.UseSkill();
                if (canCreateClone)
                {
                    canCreateClone = false;
                    player.skill.parry.EnableMirageParry(hit.transform);
                }


            }
        }
        if(stateTimer<0||triggerCalled) stateMachine.ChangeState(player.playerIdleState);
    }

    private void SuccesfulCouterAttack()
    {
        stateTimer = 10;
        player.animator.SetBool("SuccessfulCouterAttack", true);
    }
}
