using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private bool skillUsed=false;
    private float flyTime=0.25f;
    private float defaultGravity;
    

    public PlayerBlackholeState(Player _player, PlayerStateMachine _playerStateMachine, string _animateBoolName) : base(_player, _playerStateMachine, _animateBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        skillUsed=false;
        defaultGravity=rb.gravityScale;
        stateTimer=flyTime;
        rb.gravityScale=0;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale=defaultGravity;
        
        
    }

    public override void Uddate()
    {
        base.Uddate();
        if(stateTimer>0){
            rb.velocity=new Vector2(0,10);
        }
        if(stateTimer<0){
            rb.velocity=new Vector2(0,-.1f);
            if(!skillUsed){
                if(SkillManager.instance.blackhole.CanUseSkill())
                    skillUsed=true;
            }
            
        }
        if(SkillManager.instance.blackhole.SkillCompleted()) stateMachine.ChangeState(player.playerAirState);
    }

}
