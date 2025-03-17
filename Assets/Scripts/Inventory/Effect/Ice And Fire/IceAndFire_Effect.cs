using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Ice And Fire Effect",menuName ="Data/Item effect/Ice And Fire")]
public class IceAndFire_Effect : ItemEffect
{
    [SerializeField] private GameObject iceAndFirePrefab;
    [SerializeField] private float xVelocity;

    public override void ExcectEffect(Transform _target)
    {
        Player player=PlayerManage.instance.player;
        bool thirdAttack=player.playerPrimaryAttackState.comboCounter==2;
        if(thirdAttack){

            GameObject newIceAndFireEffect=Instantiate(iceAndFirePrefab,_target.transform.position,player.transform.rotation);
            newIceAndFireEffect.GetComponent<Rigidbody2D>().velocity=new Vector2(xVelocity*player.facingDir,0);
            
        }

    }
}
