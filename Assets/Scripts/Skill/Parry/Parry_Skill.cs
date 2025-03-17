using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnclockButton;
    public bool parryUnlocked {get; private set;}
    [Header("Parry recover")]
    [Range(0f,1f)]
    [SerializeField] private float restoreHealthAmount;
    [SerializeField] private UI_SkillTreeSlot parryRecoverUnlockButton;
    public bool parrryRecoverUnlocked{get; private set;}
    [Header("Parry Mirage attack")]
    [SerializeField] private UI_SkillTreeSlot parryMirageAttackUnlockButton;
    public bool parryMirageAttackUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        parryUnclockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        parryRecoverUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRecover);
        parryMirageAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryMirageAttrack);

    }
    public override void UseSkill()
    {
        base.UseSkill();
        if(parrryRecoverUnlocked){
            int restoreAmount=Mathf.RoundToInt(player.charaterStats.GetMaxHealth()*restoreHealthAmount);
            player.charaterStats.IncreaseHealBy(restoreAmount);
        }
    }
    private void UnlockParry(){
        if(parryUnclockButton.unlock) parryUnlocked=true;
    }
    private void UnlockParryRecover(){
        if(parryRecoverUnlockButton.unlock){
            
            parrryRecoverUnlocked=true;
        }
    }
    private void UnlockParryMirageAttrack(){
        if(parryMirageAttackUnlockButton.unlock) parryMirageAttackUnlocked=true;
    }
    public void EnableMirageParry(Transform _targetPosition){
        if(parryMirageAttackUnlocked){
            player.skill.clone.CreateCloneWithDelay(_targetPosition);
        }
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockParry();
        UnlockParryRecover();
        UnlockParryMirageAttrack();
    }


}
