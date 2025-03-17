using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathBringerTriggers : Enemy_AnimationFinishTrigger
{
    Enemy_DeathBringer deathBringer=>GetComponentInParent<Enemy_DeathBringer>();

    private void Relocate()=> deathBringer.FindPosition();

    private void MakeInvisable()=>deathBringer.fX.MakeTransprent(true);
    private void MakeVisable()=>deathBringer.fX.MakeTransprent(false);
}
