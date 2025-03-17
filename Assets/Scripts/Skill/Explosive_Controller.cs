using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Controller : MonoBehaviour
{
    private CharaterStats myStats;
    private float growSpeed;
    private float maxSize;
    private float explosionRadius;

    private bool canGrow=true;
    private Animator animator;

    private void Update() {
        if(canGrow)
            transform.localScale=Vector2.Lerp(transform.localScale,new Vector2(maxSize,maxSize),growSpeed*Time.deltaTime);
        if(maxSize-transform.localScale.x<0.5){
            canGrow=false;
            animator.SetTrigger("Explosion");
        }
    }

    public void SetupExplosion(float _growSpeed,float _maxSize,float _explosionRadius,CharaterStats _myStats){
        animator=GetComponent<Animator>();
        growSpeed=_growSpeed;
        maxSize=_maxSize;
        explosionRadius=_explosionRadius;
        myStats=_myStats;

    }

    public void AnimationExplodeTrigger(){
        Collider2D []colliders=Physics2D.OverlapCircleAll(transform.position,explosionRadius);
        foreach(var hit in colliders){
            if(hit.GetComponent<CharaterStats>()!=null){
                hit.GetComponent<Entity>().SetupKnockBackDir(transform);
                myStats.DoDamage(hit.GetComponent<CharaterStats>());

                }
            }
        }
    
    private void SeftDestroy()=> Destroy(gameObject);
}
