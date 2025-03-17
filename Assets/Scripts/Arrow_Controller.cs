using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [SerializeField] private string targetLayerName="Player";
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private Rigidbody2D rb;

    private CharaterStats myStat;


    private bool canMove=true;
    private bool flipped;

    private void Update() {
        
        if(canMove)
            rb.velocity=new Vector2(speed,rb.velocity.y);
    }
    public void SetupArrow(float _speed,CharaterStats _myStat){
        speed=_speed;
        myStat=_myStat;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==LayerMask.NameToLayer(targetLayerName))
        {
            myStat.DoDamage(other.GetComponent<CharaterStats>());
            StuckInto(other);

        }
        else if(other.gameObject.layer==LayerMask.NameToLayer("Ground")){
            StuckInto(other);
        }
    }

    private void StuckInto(Collider2D other)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        // other.GetComponent<CharaterStats>()?.TakeDamage(damage);
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CapsuleCollider2D>().enabled=false;
        transform.parent=other.transform;
        canMove=false;
        Destroy(this.gameObject,10);
    }

    public void FlipArrow(){
        if(flipped) return;

        speed=speed*-1;
        flipped=true;
        transform.Rotate(0,180,0);
        targetLayerName="Enemy";

    }

}
