using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject itemObject=>GetComponentInParent<ItemObject>();
    private bool canPickUp;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null&&canPickUp){
            if(other.GetComponent<CharaterStats>().isDead)
                return;

            itemObject.PickUpItem();
        }
        if(other.tag=="Ground") canPickUp=true;
    }
}
