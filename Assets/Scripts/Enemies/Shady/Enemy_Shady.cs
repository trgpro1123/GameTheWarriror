using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shady : Enemy
{

    [Header("Shady Specific")]
    [SerializeField] private GameObject explosionShadyPrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    public float moveFast;

    #region State
    public ShadyBattleState battleState{get;private set;}
    public ShadyDeadState deadState{get;private set;}
    public ShadyIdleState idleState{get;private set;}
    public ShadyMoveState moveState{get;private set;}
    public ShadyStunnedState stunnedState{get;private set;}



    #endregion   

    protected override void Awake()
    {
        base.Awake();
        battleState=new ShadyBattleState(this,stateMachine,"MoveFast",this);
        deadState=new ShadyDeadState(this,stateMachine,"Dead",this);
        idleState=new ShadyIdleState(this,stateMachine,"Idle",this);
        moveState=new ShadyMoveState(this,stateMachine,"Move",this);
        stunnedState=new ShadyStunnedState(this,stateMachine,"Stunned",this);
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
        GameObject newExplosion=Instantiate(explosionShadyPrefab,attackCheck.transform.position,Quaternion.identity);
        newExplosion.GetComponent<Explosive_Controller>().SetupExplosion(growSpeed,maxSize,attackCheckRadius,charaterStats);
        cd.enabled=false;
        rb.gravityScale=0;
    }
    public void SeftDestroy()=> Destroy(gameObject);

}