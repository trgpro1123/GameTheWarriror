using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackhole_Skill : Skill
{
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private int amountCloneAttack;
    [SerializeField] private float attackCloneCooldown;

    [Space]
    [SerializeField] private float blackholeDuration;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shinkSpeed;
    [SerializeField] private float maxSize;
    private Blackhole_Skill_Controller currentBlackhole;
    [SerializeField] private UI_SkillTreeSlot blackholeUnlockButton;
    public bool blackholeUnlocked{get;private set;}

    public bool isUsing {get;private set;}
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        isUsing=true;
        GameObject newBlackhole=Instantiate(blackholePrefab,player.transform.position,Quaternion.identity);
        currentBlackhole=newBlackhole.GetComponent<Blackhole_Skill_Controller>();
        currentBlackhole.SetUpBlackholeSkill(maxSize,growSpeed,shinkSpeed,amountCloneAttack,attackCloneCooldown,blackholeDuration);
    }

    protected override void Start()
    {
        base.Start();
        blackholeUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackhole);
    }

    protected override void Update()
    {
        base.Update();
    }
    public bool SkillCompleted(){
        if(currentBlackhole){
            if(currentBlackhole.canExitState){
                currentBlackhole=null;
                isUsing=false;
                return true;
            }

        }
        return false;

    }
    public float GetRadiusBlackHole(){
        return maxSize/2;
    }
    public void UnlockBlackhole(){
        if(blackholeUnlockButton.unlock) blackholeUnlocked=true;
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockBlackhole();
    }

}
