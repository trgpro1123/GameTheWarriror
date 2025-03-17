using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeType{Big,Medium,small}
public class Enemy_Slime : Enemy
{

    [Header("Slime Spesific")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimeToCreate;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreateVelocity;
    [SerializeField] private Vector2 maxCreateVelocity;
    #region States
    public SlimeIdleState idleState{get;private set;}
    public SlimeMoveState moveState{get;private set;}
    public SlimeStunnedState stunnedState{get;private set;}
    public SlimeDeadState deadState{get;private set;}
    public SlimeBattleState battleState{get;private set;}
    public SlimeAttackState attackState{get;private set;}
    #endregion

    protected override void Awake()
    {
        base.Awake();
        SetupDefaultFacingDir(-1);
        idleState=new SlimeIdleState(this,stateMachine,"Idle",this);
        moveState=new SlimeMoveState(this,stateMachine,"Move",this);
        stunnedState=new SlimeStunnedState(this,stateMachine,"Stun",this);
        deadState=new SlimeDeadState(this,stateMachine,"Idle",this);
        battleState=new SlimeBattleState(this,stateMachine,"Move",this);
        attackState=new SlimeAttackState(this,stateMachine,"Attack",this);

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
        if(slimeType==SlimeType.small) return;
        CreateSlime(slimePrefab);
    }
    private void CreateSlime(GameObject _slimePrefab){
        for (int i = 0; i < slimeToCreate; i++)
        {
            GameObject newSlime=Instantiate(_slimePrefab,transform.position,Quaternion.identity);
            newSlime.GetComponent<Enemy_Slime>().SetupSlime();
        }
        
    }
    public void SetupSlime(){
        float xVelocity=Random.Range(minCreateVelocity.x,maxCreateVelocity.x);
        float yVelocity=Random.Range(minCreateVelocity.y,maxCreateVelocity.y);
        isKnockBack=true;
        GetComponent<Rigidbody2D>().velocity=new Vector2(xVelocity,yVelocity);
        Invoke("CanncelKnockback",1f);
    }
    private void CanncelKnockback()=> isKnockBack=false;
    

    

    
}
