using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    
    
    public Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D circleCollider2D;
    private Player player;
    private float returnSpeed;
    private float timeFreezeDuration;
    private float timeDestroySword;
    private bool canRoutate=true;
    private bool isReturning=false;

    [Header("Bouce Info")]
    private bool isBoucing=false;
    private int bouncingAmount;
    private float speedBoucing;
    private float bouncingGravity;
    public List<Transform> enemyTarget;
    private int indexEnemy=0;


    [Header("Pierce Info")]
    private float pierceAmount=0;
    private float pierceGravity;

    [Header("Spin Info ")]
    private float hitCooldown;
    private float hitTimer;
    private float maxTravelDistance;
    private float spinDuration;
    private float spinGravity;
    private float spinTimer;
    private bool wasStopped=false;
    private bool isSpinning;

    private void Awake() {
        animator=GetComponentInChildren<Animator>();
        rb=GetComponent<Rigidbody2D>();
        circleCollider2D=GetComponent<CircleCollider2D>();

    }
    private void Update()
    {
        if (canRoutate)
            transform.right = rb.velocity;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1) player.CatchTheSword();
        }

        Boucinglogic();
        SpinLogic();

    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();

            }
            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }
                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    
                    hitTimer = hitCooldown;
                    Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (var hit in collider2Ds)
                    {
                        if (hit.GetComponent<Enemy>() != null){
                            SwordSkillDamage(hit.GetComponent<Enemy>());
                        }
                            
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void Boucinglogic()
    {
        
        if (isBoucing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[indexEnemy].position, speedBoucing * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[indexEnemy].position) <= 0.1f)
            {
                SwordSkillDamage(enemyTarget[indexEnemy].GetComponent<Enemy>());
                indexEnemy++;
                bouncingAmount--;
                if (bouncingAmount <= 0 ||indexEnemy >= enemyTarget.Count)
                {
                    isReturning = true;
                    isBoucing = false;
                    indexEnemy = 0;
                }
                // if (indexEnemy >= enemyTarget.Count) indexEnemy = 0;

            }
        }
    }

    public void SetupSword(Vector2 _launch,float _gravityScale,Player _player,float _returnSpeed,float _timeFreezeDuration,float _timeDestroySword){
        player=_player;
        rb.velocity=_launch;
        rb.gravityScale=_gravityScale;
        returnSpeed=_returnSpeed;
        timeFreezeDuration=_timeFreezeDuration;
        timeDestroySword=_timeDestroySword;
        if(pierceAmount<=0)
            animator.SetBool("Rotation",true);

        Destroy(gameObject,timeDestroySword);

    }

    public void ReturnSword(){
        rb.constraints=RigidbodyConstraints2D.FreezeAll;
        transform.parent=null;
        isReturning=true;
        
    }

    public void SetUpBouncingSkill(bool _isBouncing,int _bouncingAmount,float _speedBoucing,float _bouncingGravity){
        isBoucing=_isBouncing;
        bouncingAmount=_bouncingAmount;
        bouncingGravity=_bouncingGravity;
        speedBoucing=_speedBoucing;
        enemyTarget=new List<Transform>();
        

    }
    public void SetUpPiercingSkill(int _pierceAmount){
        pierceAmount=_pierceAmount;
    }
    public void SetUpSpinningSkill(bool _isSpinning,float _maxTravelDistance,float _spinDuration,float _spinGravity,float _hitCooldown){
        isSpinning=_isSpinning;
        maxTravelDistance=_maxTravelDistance;
        spinDuration=_spinDuration;
        spinGravity=_spinGravity;
        hitCooldown=_hitCooldown;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isReturning) return;
        SetupTargetsForBounce(other);
        StuckInto(other);
    }

    private void SetupTargetsForBounce(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if(isBoucing&&enemyTarget.Count<=0){
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in collider2Ds)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTarget.Add(hit.transform);
                }
            }
        }
    }

    private void StuckInto(Collider2D other)
    {
        if(other.GetComponent<Enemy>()!=null){
            SwordSkillDamage(other.GetComponent<Enemy>());
        }
        
        if(pierceAmount>0&&other.GetComponent<Enemy>()!=null)  return;
        if(isSpinning){
            StopWhenSpinning();
            return;
        }
        canRoutate = false;
        rb.isKinematic = true;
        circleCollider2D.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if(isBoucing&&enemyTarget.Count>0) return;

        transform.parent = other.transform;
        animator.SetBool("Rotation", false);
    }
    private void SwordSkillDamage(Enemy _enemy){
        EnemyStats enemyStats=_enemy.GetComponent<EnemyStats>();
        player.charaterStats.DoDamage(enemyStats);
        if(player.skill.sword.timeStopUnlocked)
            _enemy.FreezeTimerFor(timeFreezeDuration);
        if(player.skill.sword.timeStopVunerabilityUnlocked)
            player.charaterStats.MakeVulnareableFor(timeFreezeDuration);
        //enemy.StartCoroutine("FreezeTimerCoroutine",0.2f);
        ItemData_Equipment equipment=Inventory.instance.GetEquiment(EquipmentType.Amulet);
            if(equipment!=null){
                equipment.ItemEffect(_enemy.transform);
            }
    }
}
