using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Controller : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    
    private float crystalSpeed;
    private float crystalDutarion;
    private float growSpeed;
    private bool canMove;
    private bool canExplode;
    private bool canGrow;
    private float crystalTimer;
    private Transform closestEnemy=null;
    private Animator animator;
    private CircleCollider2D circleCollider2D;
    private Player player;

    private void Awake() {
        animator=GetComponent<Animator>();
        circleCollider2D=GetComponent<CircleCollider2D>();
    }


    private void Update() {
        crystalTimer-=Time.deltaTime;
        if(crystalTimer<0){
            FinishCrystal();
        }
        if(canGrow){
            transform.localScale=Vector2.Lerp(transform.localScale,new Vector2(3,3),growSpeed*Time.deltaTime);
        }
        if(canMove&&closestEnemy!=null){
            transform.position=Vector2.MoveTowards(transform.position,closestEnemy.position,crystalSpeed*Time.deltaTime);

            if(Vector2.Distance(transform.position,closestEnemy.position)<0.5f){
                FinishCrystal();
                canMove=false;
            }
        }
    }
    public void SetUpCrystal(float _crystalSpeed,float _growSpeed,float _crystalDuration,bool _canMove,bool _canExplode,Transform _closestEnemy,Player _player){
        crystalSpeed=_crystalSpeed;
        crystalTimer=_crystalDuration;
        growSpeed=_growSpeed;
        closestEnemy=_closestEnemy;
        canMove=_canMove;
        canExplode=_canExplode;
        player=_player;
    }
    public void FinishCrystal(){
        if(canExplode){
            animator.SetTrigger("Explode");
            canGrow=true;
            canMove=false;
        }
        else DestroySeft();
    }
    public void DestroySeft()=>Destroy(gameObject);
    public void AnimationExplodeTrigger(){
        Collider2D []colliders=Physics2D.OverlapCircleAll(transform.position,circleCollider2D.radius);
        foreach(var hit in colliders){
            if(hit.GetComponent<Enemy>()!=null){
                hit.GetComponent<Entity>().SetupKnockBackDir(transform);
                player.charaterStats.DoMagicDamage(hit.GetComponent<CharaterStats>());
                ItemData_Equipment equipment=Inventory.instance.GetEquiment(EquipmentType.Amulet);
                if(equipment!=null){
                    equipment.ItemEffect(hit.transform);
                }
            }
        }
    }
    public void ChooseRandomEnemy(){
        float radius=SkillManager.instance.blackhole.GetRadiusBlackHole();
        Collider2D []colliders=Physics2D.OverlapCircleAll(transform.position,radius,whatIsEnemy);
        if(colliders.Length>0)
            closestEnemy=colliders[Random.Range(0,colliders.Length)].transform;
    }

    

}
