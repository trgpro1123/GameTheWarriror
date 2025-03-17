using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerCastSpell_Controller : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;
    private CharaterStats myStats;

    public void SetUpSpell(CharaterStats _myStats)=>myStats=_myStats;
    public void AnimationTrigger(){
        Collider2D []colliders=Physics2D.OverlapBoxAll(check.position,boxSize,whatIsPlayer);
        foreach(var hit in colliders){
            if(hit.GetComponent<PlayerStats>()!=null){
                hit.GetComponent<Entity>().SetupKnockBackDir(transform);
                myStats.DoDamage(hit.GetComponent<CharaterStats>());

            }
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(check.position,boxSize);
    }
    public void SeftDestroy()=>Destroy(gameObject);
    
}
