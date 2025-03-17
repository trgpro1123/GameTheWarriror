using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Thunder Explosion Effect",menuName ="Data/Item effect/Thunder Explosion")]
public class ThunderEplosion_Effect : ItemEffect
{
    [SerializeField] private GameObject thunderExposionPrefab;
    public override void ExcectEffect(Transform _target)
    {
        GameObject newThunderExplosion=Instantiate(thunderExposionPrefab,_target.position,Quaternion.identity);
        Destroy(newThunderExplosion,1);


    }
}
