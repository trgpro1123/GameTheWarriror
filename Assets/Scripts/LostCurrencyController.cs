using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostCurrencyController : MonoBehaviour
{
    public int currency;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null){
            PlayerManage.instance.currency+=currency;
            Destroy(this.gameObject);
        }
    }
}
