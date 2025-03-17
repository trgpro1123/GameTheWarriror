using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderExplosion_Controller : MonoBehaviour
{
    

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()!=null){
            PlayerStats player=PlayerManage.instance.player.GetComponent<PlayerStats>();
            EnemyStats enemyStats=other.GetComponent<EnemyStats>();

            player.DoMagicDamage(enemyStats,1.2f);
        }
    }
}
