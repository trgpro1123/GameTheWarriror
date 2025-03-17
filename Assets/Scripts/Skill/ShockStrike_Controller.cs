using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockStrike_Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;


    [SerializeField]private CharaterStats charaterStats=null;
    [SerializeField]private Animator animator;
    private int damage;
    private bool trigger=false;

    private void Start() {
        animator=GetComponentInChildren<Animator>();
    }
    private void Update() {
        if(!charaterStats) Debug.Log(10);
        if(charaterStats==null) return;
        if(trigger) return;
        transform.position=Vector2.MoveTowards(transform.position,charaterStats.transform.position,moveSpeed*Time.deltaTime);
        transform.right=transform.position-charaterStats.transform.position;
        if(Vector2.Distance(transform.position,charaterStats.transform.position)<.1f){
            animator.transform.localRotation=Quaternion.identity;
            animator.transform.localPosition=new Vector3(0,0.5f);

            transform.localRotation=Quaternion.identity;
            transform.localScale=new Vector3(3,3);
            trigger=true;
            animator.SetTrigger("Hit");
            Invoke("DamageAndSeftDestroy",.1f);

        }
    }
    public void SetUpShockStrike(int _damage,CharaterStats _charaterStats){
        damage=_damage;
        charaterStats=_charaterStats;
    }

    private void DamageAndSeftDestroy(){
        charaterStats.TakeDamage(damage);
        Destroy(gameObject,0.5f);
    }
}
