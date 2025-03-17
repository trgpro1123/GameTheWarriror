using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown;
    protected Player player;
    public float cooldownTimer;

    protected virtual void Start() {
        player=PlayerManage.instance.player;
        Invoke("CheckUnlock",.1f);
        //CheckUnlock();
    }
    protected virtual void Update() {
        cooldownTimer-=Time.deltaTime;
    }
    public virtual bool CanUseSkill(){
        if(cooldownTimer<0){
            UseSkill();
            cooldownTimer=cooldown;
            return true;
        }
        player.fX.CreatePopUpText("CoolDown");
        return false;
    }
    public virtual void UseSkill(){
        
    }
    public Transform FindClosestEnemy(Transform _transform){
        Collider2D []colliders=Physics2D.OverlapCircleAll(_transform.position,25);
        float closestDistance=Mathf.Infinity;
        Transform closestEnemy=null;
        foreach(var hit in colliders){
            if(hit.GetComponent<Enemy>()!=null){
                float distanceToEnemy=Vector2.Distance(_transform.position,hit.transform.position);
                if(distanceToEnemy<closestDistance){
                    closestDistance=distanceToEnemy;
                    closestEnemy=hit.transform;

                }
            }
        }
        return closestEnemy;
    }
    protected virtual void CheckUnlock(){

    }
    
}
