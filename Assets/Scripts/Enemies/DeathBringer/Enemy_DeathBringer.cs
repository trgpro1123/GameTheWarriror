using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathBringer : Enemy
{

    [Header("Cast spell Detail")]
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Vector2 offset;
    public int numberCastSpell;
    public float cooldownSpell;
    public float lastTimeCastSpell;

    [SerializeField] private float stateSpellCooldown;
    
    [Header("Teleport Detail")]
    [SerializeField] private BoxCollider2D area;
    [SerializeField] private Vector2 surroundingCheckSize;
    public int changeTeleport=25;
    public int defaultChangeTeleport=25;

    #region State

    public DeathBringerIdleState idleState{get;private set;}
    public DeathBringerMoveState moveState{get;private set;}
    public DeathBringerAttackState attackState{get;private set;}
    public DeathBringerDeadState deadState{get;private set;}
    public DeathBringerBattleState battleState{get;private set;}
    public DeathBringerSpellCastState spellCastState{get;private set;}
    public DeathBringerTeleportState teleportState{get;private set;}
    

    #endregion

    public bool startBattle;

    protected override void Awake()
    {
        base.Awake();
        SetupDefaultFacingDir(-1);
        idleState=new DeathBringerIdleState(this,stateMachine,"Idle",this);
        moveState=new DeathBringerMoveState(this,stateMachine,"Move",this);
        attackState=new DeathBringerAttackState(this,stateMachine,"Attack",this);
        deadState=new DeathBringerDeadState(this,stateMachine,"Idle",this);
        battleState=new DeathBringerBattleState(this,stateMachine,"Move",this);
        spellCastState=new DeathBringerSpellCastState(this,stateMachine,"SpellCast",this);
        teleportState=new DeathBringerTeleportState(this,stateMachine,"Teleport",this);

    }


    protected override void Start() {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update() {
        base.Update();
        
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    public void FindPosition(){
        float x=Random.Range(area.bounds.min.x+3,area.bounds.max.x-3);
        float y=Random.Range(area.bounds.min.y+3,area.bounds.max.y-3);

        transform.position=new Vector3(x,y);
        transform.position=new Vector3(transform.position.x,transform.position.y - GroundBelow().distance +(cd.size.y)/2);

        if(!GroundBelow()||SomethingIsGround()){
            FindPosition();
        }
    }
    private RaycastHit2D GroundBelow()=>Physics2D.Raycast(transform.position,Vector2.down,100,whatIsGround);

    private bool SomethingIsGround() => Physics2D.BoxCast(transform.position,surroundingCheckSize,0,Vector2.zero,0,whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position,surroundingCheckSize);
    }

    
    public bool CanTeleport(){
        if(Random.Range(0,100)<=changeTeleport){
            changeTeleport=defaultChangeTeleport;
            return true;
        }
        changeTeleport+=5;
        return false;
    }

    public bool CanCastSpell(){
        if(Time.time>lastTimeCastSpell+stateSpellCooldown){
            return true;
        }
        return false;
    }

    public void CreateSpell(){
        Player player=PlayerManage.instance.player;
        
        float xOffset=0;
        if(player.rb.velocity.x!=0)
            xOffset=player.facingDir*3+Random.Range(offset.x,offset.y)*player.facingDir;
        
        Vector2 spellPosition=new Vector2(player.transform.position.x+xOffset,player.transform.position.y+1.5f);

        GameObject newSpell=Instantiate(spellPrefab,spellPosition,Quaternion.identity);
        newSpell.GetComponent<DeathBringerCastSpell_Controller>().SetUpSpell(charaterStats);
    }


}

