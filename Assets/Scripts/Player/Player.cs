using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{

    [Header("Attack Info")]
    public Vector2[] attackMovement;
    public float counterAttakDuration=0.5f;

    [Header("Move Info")]
    public float moveSpeed=8f;
    public float jumpForce=15f;
    public float swordReturnImpact=10f;

    [Header("Dash Info")]
    public float dashSpeed=15f;
    public float dashDuration=0.2f;
    public float dashDir{get;private set;}
    
    

    private float defaultMoveSpeed;
    private float defaultJumpForce;
    private float defaultDash;

    
    
    



    public PlayerStateMachine stateMachine {get;private set;}
    public PlayerMoveState playerMoveState {get;private set;}
    public PlayerIdleState playerIdleState {get;private set;}
    public PlayerJumpState playerJumpState {get;private set;}
    public PlayerAirState playerAirState {get;private set;}
    public PlayerDashState playerDashState {get;private set;}
    public PlayerWallSliderState playerWallSliderState {get;private set;}
    public PlayerWallJumpState playerWallJumpState {get;private set;}
    public PlayerPrimaryAttackState playerPrimaryAttackState {get;private set;}
    public PlayerCounterState playerCounterState {get;private set;}
    public PlayerAimSwordState playerAimSwordState{get;private set;}
    public PlayerCatchSwordState playerCatchSwordState{get;private set;}
    public PlayerBlackholeState playerBlackholeState{get;private set;}
    public PlayerDeathState playerDeathState{get;private set;}
    public bool isBusy {get;private set;}
    public SkillManager skill;
    public GameObject sword{get;private set;}
    public PlayerFX fX{get;private set;}


    

    protected override void Awake() {
        base.Awake();
        stateMachine=new PlayerStateMachine();
        playerIdleState=new PlayerIdleState(this,stateMachine,"Idle");
        playerMoveState=new PlayerMoveState(this,stateMachine,"Move");
        playerJumpState=new PlayerJumpState(this,stateMachine,"Jump");
        playerAirState=new PlayerAirState(this,stateMachine,"Jump");
        playerDashState=new PlayerDashState(this,stateMachine,"Dash");
        playerWallSliderState=new PlayerWallSliderState(this,stateMachine,"WallSlide");
        playerWallJumpState=new PlayerWallJumpState(this,stateMachine,"Jump");
        playerPrimaryAttackState =new PlayerPrimaryAttackState(this,stateMachine,"Attack");
        playerCounterState=new PlayerCounterState(this,stateMachine,"CounterAttack");
        playerAimSwordState=new PlayerAimSwordState(this,stateMachine,"AimSword");
        playerCatchSwordState=new PlayerCatchSwordState(this,stateMachine,"ThrowSword");
        playerCatchSwordState=new PlayerCatchSwordState(this,stateMachine,"CatchSword");
        playerBlackholeState=new PlayerBlackholeState(this,stateMachine,"Jump");
        playerDeathState=new PlayerDeathState(this,stateMachine,"Die");
        
        
    }
    protected override void Start() {
        base.Start();
        fX=GetComponent<PlayerFX>();
        skill=SkillManager.instance;
        stateMachine.Initialize(playerMoveState);
        defaultMoveSpeed=moveSpeed;
        defaultJumpForce=jumpForce;
        defaultDash=dashSpeed;
        
    }
    protected override void Update() {
        base.Update();
        stateMachine.playerState.Uddate();
        if(Input.GetKeyDown(KeyCode.F)&&skill.crystal.crystalSimpleUnlocked) skill.crystal.CanUseSkill();
        Dashing();
        if(Input.GetKeyDown(KeyCode.Alpha1)) Inventory.instance.CanUseFlask();
    }
    public IEnumerator Busy(float _time){
        isBusy=true;
        yield return new WaitForSeconds(_time);
        isBusy=false;

    }
    public void AnimatorTrigger() => stateMachine.playerState.AnimationFininshTrigger();
    public void Dashing(){
        if(IsWallDetected()) return;
        if(skill.blackhole.isUsing) return;
        if(skill.dash.dashUnlocked==false) return;
        if(Input.GetKeyDown(KeyCode.LeftShift)&&SkillManager.instance.dash.CanUseSkill()){
            dashDir=Input.GetAxisRaw("Horizontal");
            if(dashDir==0) dashDir=facingDir;
            stateMachine.ChangeState(playerDashState);
        } 
    }
    public void AssignNewSwrod(GameObject _newSword){
        sword=_newSword;
    }
    public void CatchTheSword(){
        stateMachine.ChangeState(playerCatchSwordState);
        Destroy(sword);
    }
    public override void Die(){
        base.Die();
        stateMachine.ChangeState(playerDeathState);
    }
    public override void SlowEtityBy(float _slowPercentage, float _slowDration)
    {
        base.SlowEtityBy(_slowPercentage, _slowDration);
        moveSpeed*=(1-_slowPercentage);
        jumpForce*=(1-_slowPercentage);
        dashSpeed*=(1-_slowPercentage);
        animator.speed*=(1-_slowPercentage);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed=defaultMoveSpeed;
        jumpForce=defaultJumpForce;
        dashSpeed=defaultDash;

    }
    protected override void SetupZeroKnockBack()
    {
        knockBackPower=Vector2.zero;
    }
    










    

}
