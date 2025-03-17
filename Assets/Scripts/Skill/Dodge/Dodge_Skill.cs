using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dodge_Skill : Skill
{
    [Header("Dodge")]
    [SerializeField] private UI_SkillTreeSlot dodgeUnlockButton;
    public bool dodgeUnlocked {get;private set;}
    [Header("Dodge Mirage")]
    [SerializeField] private UI_SkillTreeSlot dodgeMirageUnlockButton;
    public bool dodgeMirageUnlocked{get;private set;}

    protected override void Start()
    {
        base.Start();
        dodgeUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        dodgeMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDodgeMirage);
    }

    public void UnlockDodge(){
        if(dodgeUnlockButton.unlock&&dodgeUnlocked==false){
            player.charaterStats.evasion.AddModifier(20);
            Inventory.instance.UpdateStatsUI();
            dodgeUnlocked=true;
        }
    }
    public void UnlockDodgeMirage(){
        if(dodgeMirageUnlockButton.unlock) dodgeMirageUnlocked=true;
    }
    public void CreateMirageOnDodge(){
        if(dodgeMirageUnlocked)
            player.skill.clone.CreateClone(player.transform,new Vector2(2*player.facingDir,0));
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockDodge();
        UnlockDodgeMirage();
        CreateMirageOnDodge();
    }
}
