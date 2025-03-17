using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
[RequireComponent(typeof(ItemDrop))]
public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [Header("Stunned Info")]
    public float stunDuration=1f;
    public Vector2 stunDirection=new Vector2(2,2);
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move Info")]
    public float moveSpeed=2f;
    public float idleTime=1f;
    private float defaultSpeed;
    [Header("Attack Info")]
    public float agroDistance=2f;
    public float attackDistance;
    public float attackCoolDown=0.4f;
    public float minAttackCoolDown;
    public float maxAttackCoolDown;
    public float battleTime=1.5f;
    [HideInInspector] public float lastTimeAttack;
    public EntityFX fX{get;private set;}
    public string lastAnimBoolName {get;private set;}



    


    public EnemyStateMachine stateMachine {get;private set;}
    



    protected override void Awake() {
        base.Awake();
        stateMachine=new EnemyStateMachine();
    }
    protected override void Start() {
        base.Start();
        fX=GetComponent<EntityFX>();
        defaultSpeed=moveSpeed;
    }
    protected override void Update() {
        base.Update();
        stateMachine.enemyState.Uddate();
    }
    public RaycastHit2D IsPlayerDetected()=>Physics2D.Raycast(transform.position,Vector2.right*facingDir,attackDistance+5,whatIsPlayer);

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();
        Gizmos.color=Color.red;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x+attackDistance*facingDir,transform.position.y));
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x+(attackDistance+5)*facingDir,transform.position.y+1));
    }
    public void AnimatorTrigger() => stateMachine.enemyState.AnimationFininshTrigger();

    public void OpenCounterAttackWindown(){
        canBeStunned=true;
        counterImage.SetActive(true);
    }
    public void CloseCounterAttackWindown(){
        canBeStunned=false;
        counterImage.SetActive(false);
    }
    public virtual bool CanBeStunned(){
        if(canBeStunned){
            CloseCounterAttackWindown();
            return true;
        }
        return false;
    }
    public virtual void FreezeTime(bool _isForzer){
        if(_isForzer){
            moveSpeed=0;
            animator.speed=0;
        }else{
            moveSpeed=defaultSpeed;
            animator.speed=1;
        }
    }
    public void FreezeTimerFor(float _duration){
        StartCoroutine(FreezeTimerCoroutine(_duration));
    }
    public virtual IEnumerator FreezeTimerCoroutine(float _time){
        FreezeTime(true);
        yield return new WaitForSeconds(_time);
        FreezeTime(false);
    }
    public void AssignLastAnimBoolName(string _lastAnimBoolName){
        lastAnimBoolName=_lastAnimBoolName;
    }
    public override void SlowEtityBy(float _slowPercentage, float _slowDration)
    {
        base.SlowEtityBy(_slowPercentage, _slowDration);
        moveSpeed*=(1-_slowPercentage);
        animator.speed*=(1-_slowPercentage);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed=defaultSpeed;
    }
    public virtual void AnimationSpecialAttackTrigger(){

    }
}
