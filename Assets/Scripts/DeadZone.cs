using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<CharaterStats>()!=null){
            other.GetComponent<CharaterStats>().KillYourSelf();
        }
        else{
            Destroy(other.gameObject);
        }
    }
}
