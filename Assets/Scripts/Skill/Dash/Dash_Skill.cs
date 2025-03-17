using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dash_Skill : Skill
{
    [Header("Dash")]
    [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
    public bool dashUnlocked;

    [Header("Dash - Clone")]
    [SerializeField] private UI_SkillTreeSlot dashCloneUnlockButton;
    public bool dashCloneUnlocked{get; private set;}

    [Header("Dash - Double dash")]
    [SerializeField] private UI_SkillTreeSlot doubleDashCloneUnlcokButton;
    public bool doubleDashCloneUnlocked{get; private set;}

    protected override void Start()
    {
        base.Start();
        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        dashCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDashClone);
        doubleDashCloneUnlcokButton.GetComponent<Button>().onClick.AddListener(UnlocDashkDoubleClone);
    }
 

    public override void UseSkill()
    {
        base.UseSkill();
        
    }


    private void UnlockDash(){
        if(dashUnlockButton.unlock) dashUnlocked=true;
    }
    private void UnlockDashClone(){
        if(dashCloneUnlockButton.unlock) dashCloneUnlocked=true;
    }
    private void UnlocDashkDoubleClone(){
        if(dashCloneUnlockButton.unlock) doubleDashCloneUnlocked=true;
    }

    public void CreateCloneStart(){
        if(dashCloneUnlocked) SkillManager.instance.clone.CreateClone(player.transform,Vector3.zero);
    }
    public void CreateCloneOver(){
        if(doubleDashCloneUnlocked) SkillManager.instance.clone.CreateClone(player.transform,Vector3.zero);
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockDash();
        UnlockDashClone();
        UnlocDashkDoubleClone();
        CreateCloneStart();
        CreateCloneOver();
    }


}
