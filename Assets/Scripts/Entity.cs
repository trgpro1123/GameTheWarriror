using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    #region Components
    public Animator animator {get;private set;}
    public Rigidbody2D rb{get;private set;}
    public SpriteRenderer sp{get;private set;}
    
    public CharaterStats charaterStats {get;private set;}
    public CapsuleCollider2D cd{get;private set;}
    #endregion

    [Header("Collison Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance=0.4f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance=1f;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("KnockBack Info")]
    [SerializeField] protected Vector2 knockBackPower=new Vector2(5,5);
    [SerializeField] protected Vector2 knockBackOffset;
    [SerializeField] protected  float knockBackDuration=0.1f;
    protected bool isKnockBack=false;




    protected bool facingRight =true;
    public int facingDir{get;private set;} =1;
    public int knockBackdir{get;private set;}
    public System.Action onFlipped;
    


    protected virtual void Awake() {
        
    }
    protected virtual void Start() {
        animator=GetComponentInChildren<Animator>();
        rb=GetComponent<Rigidbody2D>();
        
        sp=GetComponentInChildren<SpriteRenderer>();
        charaterStats=GetComponent<CharaterStats>();
        cd=GetComponent<CapsuleCollider2D>();

    }
    protected virtual void Update(){

    }

    public void DamageImpact()=>StartCoroutine(HitKnockBack());
    public void SetupKockBackPower(Vector2 _power)=> knockBackPower=_power;
    public void SetupKnockBackDir(Transform _dirImpact){
        if(transform.position.x<_dirImpact.position.x) knockBackdir=-1;
        else knockBackdir=1;
    }

    private IEnumerator HitKnockBack(){
        isKnockBack=true;

        float xOffset=Random.Range(knockBackOffset.x,knockBackOffset.y);

        rb.velocity=new Vector2((knockBackdir+xOffset)*knockBackPower.x,knockBackPower.y);
        yield return new WaitForSeconds(knockBackDuration);
        isKnockBack=false;
        SetupZeroKnockBack();
    }
    #region Velocity
    public void ZeroVelocity(){
        if(isKnockBack) return;
        rb.velocity=new Vector2(0,0);
    }
    public void SetRigidbody(float _x,float _y){
        if(isKnockBack) return;
        rb.velocity=new Vector2(_x,_y);
        FlipController(_x);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected()=>Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,whatIsGround);
    public virtual bool IsWallDetected()=>Physics2D.Raycast(wallCheck.position,Vector2.right*facingDir,wallCheckDistance,whatIsGround);
    protected virtual void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position,new Vector2(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,new Vector2(wallCheck.position.x+wallCheckDistance,wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position,attackCheckRadius);
    }
    #endregion
    
    #region Flip
    public virtual void Flip(){
        facingDir*=(-1);
        facingRight=!facingRight;
        transform.Rotate(0,180,0);
        onFlipped();
    }
    public virtual void FlipController(float _x){
        if(_x>0&&!facingRight) Flip();
        else if(_x<0&&facingRight) Flip();
    }
    #endregion
    

    public virtual void Die(){

    }
    public virtual void SlowEtityBy(float _slowPercentage,float _slowDration){
        Invoke("ReturnDefaultSpeed",_slowDration);
    }
    protected virtual void ReturnDefaultSpeed(){
        animator.speed=1;
    }
    protected virtual void SetupZeroKnockBack(){

    }
    public virtual void SetupDefaultFacingDir(int _direction){
        facingDir=_direction;
        if(facingDir==-1){
            facingRight=false;
        }
    }
    
}
