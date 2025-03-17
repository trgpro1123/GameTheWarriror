using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    
    [SerializeField] private float colorLossingSpeed;
    [SerializeField] Transform attackCheck;
    [SerializeField] float attackCheckRadius;
    private float cloneTimer;
    private float cloneDuration;
    private SpriteRenderer sr;
    private Animator animator;
    private Transform closestEnemy;
    private int faceDir=1;
    private bool canDuplicateClone;
    private int changeToDuplicate;
    private Player player;
    private float attackMultiplier;
    private void Awake() {
        sr=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
    }

    private void Update() {
        cloneTimer-=Time.deltaTime;
        if(cloneTimer<0){
            sr.color=new Color(1,1,1,sr.color.a-(Time.deltaTime*colorLossingSpeed));
            if(sr.color.a<=0) Destroy(gameObject);
        }
    }
    public void SetUpClone(Transform _playerPosition,float _time,bool _canAttack,Vector3 _offset,bool _canDuplicateClone,int _changeToDuplicate,Transform _closestEnemy,Player _player,float _attackMultiplier){
        if(_canAttack) animator.SetInteger("AttackNumber",Random.Range(1,3));
        transform.position=_playerPosition.position+_offset;
        cloneTimer=_time;
        cloneDuration=_time;
        closestEnemy=_closestEnemy;
        canDuplicateClone=_canDuplicateClone;
        changeToDuplicate=_changeToDuplicate;
        player=_player;
        attackMultiplier=_attackMultiplier;
        FaceClosestTarget();

    }
    private void AnimationTrigger(){
        cloneTimer=cloneDuration;
    }
    public void AttackTrigger(){
        Collider2D []colliders=Physics2D.OverlapCircleAll(attackCheck.position,attackCheckRadius);
        foreach(var hit in colliders){
            if(hit.GetComponent<Enemy>()!=null){
                hit.GetComponent<Entity>().SetupKnockBackDir(transform);
                PlayerStats playerStats=player.GetComponent<PlayerStats>();
                EnemyStats enemyStats=hit.GetComponent<EnemyStats>();
                playerStats.DoDamageClone(enemyStats,attackMultiplier);
                if(player.skill.clone.agressiveMirageUnlocked){
                    ItemData_Equipment dataEquiment= Inventory.instance.GetEquiment(EquipmentType.Weapon);
                    if(dataEquiment!=null) dataEquiment.ItemEffect(hit.transform);
                    playerStats.DoMagicDamage(enemyStats);
                }
                if(canDuplicateClone){
                    if(Random.Range(0,100)<changeToDuplicate){
                        SkillManager.instance.clone.CreateClone(hit.transform,new Vector3(2*faceDir,0));
                    }
                }
            }
        }
    }
    private void FaceClosestTarget(){
        if(closestEnemy!=null){
            if(transform.position.x>closestEnemy.position.x){
                faceDir=-1;
                transform.Rotate(0,180,0);
            }
        }
    }
}
