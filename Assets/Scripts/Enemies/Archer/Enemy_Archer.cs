using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer : Enemy
{
    
    [Header("Archer spesific")]
    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private float speedArrow;
    public Vector2 JumpForce;
    public float jumpCooldown;
    public float safeDistance;
    [HideInInspector] public float lasTimeJump;

    [SerializeField] private Transform groundBehindCheck;
    [SerializeField] private Vector2 groundBehinhCheckSize;

    #region State
    public ArcherIdleState idleState{get;private set;}
    public ArcherMoveState moveState{get;private set;}
    public ArcherAttackState attackState{get;private set;}
    public ArcherBattleState battleState{get;private set;}
    public ArcherDeadState deadState{get;private set;}
    public ArcherJumpState jumpState{get;private set;}
    public ArcherStunnedState stunnedState{get;private set;}
    #endregion


    protected override void Awake()
    {
        base.Awake();
        idleState=new ArcherIdleState(this,stateMachine,"Idle",this);
        moveState=new ArcherMoveState(this,stateMachine,"Move",this);
        attackState=new ArcherAttackState(this,stateMachine,"Attack",this);
        battleState=new ArcherBattleState(this,stateMachine,"Idle",this);
        deadState=new ArcherDeadState(this,stateMachine,"Move",this);
        jumpState=new ArcherJumpState(this,stateMachine,"Jump",this);
    }



    protected override void Start() {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update() {
        base.Update();
        
    }

    public override bool CanBeStunned(){
        if(base.CanBeStunned()){
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    public override void AnimationSpecialAttackTrigger()
    {
        GameObject newArrow=Instantiate(arrowPrefab,attackCheck.position,transform.rotation);
        newArrow.GetComponent<Arrow_Controller>().SetupArrow(speedArrow*facingDir,charaterStats);
    }

    public bool GroundCheckBehind()=> Physics2D.BoxCast(groundBehindCheck.position,groundBehinhCheckSize,0,Vector2.zero,0,whatIsGround);
    public bool WallBehind()=> Physics2D.Raycast(wallCheck.position,Vector2.right*-facingDir,wallCheckDistance+2,whatIsGround);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(groundBehindCheck.position,groundBehinhCheckSize);
    }
}


