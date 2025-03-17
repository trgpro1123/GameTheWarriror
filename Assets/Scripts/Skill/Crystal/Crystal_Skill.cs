using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_Skill : Skill
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private float crystallDuration;
    [SerializeField] private float growSpeed;


    [Header("Crystal simple")]
    [SerializeField] private UI_SkillTreeSlot crystalSimpleUnlockButton;
    public bool crystalSimpleUnlocked{get;private set;}

    [Header("Crystal Mirage")]
    [SerializeField] private bool cloneInsteadOfCrystal;
    [SerializeField] private UI_SkillTreeSlot crystalMirageUnlockButton;
    public bool crystalMirageUnlocked;
    [Header("Moving Crystal")]
    [SerializeField] private bool canMove;   
    [SerializeField] private float crystalSpeed;
    [SerializeField] private UI_SkillTreeSlot crystalMovingUnlockButton;

    [Header("Explosive Crystal")]
    [SerializeField] private bool canExplode;
    [SerializeField] private UI_SkillTreeSlot crystalExplosionUnlockButton;

    [Header("Mutil Crystal")]
    [SerializeField] private bool canUseMutilStacks;
    [SerializeField] private int amountCrystalStacks;
    [SerializeField] private float mutilStackCoodown;
    [SerializeField] private float useTimeWindown;
    [SerializeField] private UI_SkillTreeSlot crystalMutilCrystalUnlockButton;
    [SerializeField] private List<GameObject> crystalLeft;


    protected override void Start()
    {
        base.Start();
        crystalSimpleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalSimple);
        crystalMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMirage);
        crystalMovingUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMoving);
        crystalExplosionUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalExplosion);
        crystalMutilCrystalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMultiCrystal);
    }


    private GameObject currentCrystal;
    public override bool CanUseSkill()
    {
        if(currentCrystal!=null){
            UseSkill();
            return true;

        }
        return base.CanUseSkill();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        if(CanUseMutilCrystal()) return;
        if(currentCrystal==null){
            CreateCrystal();
        }else{
            if(canMove) return;
            Vector3 playerPos=player.transform.position;
            player.transform.position=currentCrystal.transform.position;
            currentCrystal.transform.position=playerPos;
            if(cloneInsteadOfCrystal){
                player.skill.clone.CreateClone(currentCrystal.transform,Vector3.zero);
                Destroy(currentCrystal);
            }else{
                currentCrystal.GetComponent<Crystal_Controller>()?.FinishCrystal();

            }

        }
        
        
    }
    public void CreateCrystal(){
        currentCrystal=Instantiate(crystalPrefab,player.transform.position,Quaternion.identity);
        Crystal_Controller crystal_Controller=currentCrystal.GetComponent<Crystal_Controller>();
        crystal_Controller.SetUpCrystal(crystalSpeed,growSpeed,crystallDuration,canMove,canExplode,FindClosestEnemy(currentCrystal.transform),player);
    }
    public bool CanUseMutilCrystal(){
        if(canUseMutilStacks){
            if(crystalLeft.Count>0){
                if(crystalLeft.Count==amountCrystalStacks)
                    Invoke("ResetAbility",useTimeWindown);
                cooldown=0;
                GameObject crystalSpawn=crystalLeft[crystalLeft.Count-1];
                GameObject newCrystal=Instantiate(crystalSpawn,player.transform.position,Quaternion.identity);
                crystalLeft.Remove(crystalSpawn);
                newCrystal.GetComponent<Crystal_Controller>().SetUpCrystal(crystalSpeed,growSpeed,crystallDuration,canMove,canExplode,FindClosestEnemy(newCrystal.transform),player);
                if(crystalLeft.Count<=0){
                    cooldown=mutilStackCoodown;
                    RefiCrystal();
                }
                return true;
            }
        }
        return false;
    }
    private void RefiCrystal(){
        int amountAdd=amountCrystalStacks-crystalLeft.Count;
        for (int i = 0; i < amountAdd; i++)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }
    private void ResetAbility(){
        if(cooldownTimer>0) return;
        cooldown=mutilStackCoodown;
        RefiCrystal();
    }
    public void CurrentCrystalChooseRandomEnemy(){
            currentCrystal.GetComponent<Crystal_Controller>().ChooseRandomEnemy();
    }
    public void UnlockCrystalMirage(){
        if(crystalMirageUnlockButton.unlock) cloneInsteadOfCrystal=true;
    }
    public void UnlockCrystalMoving(){
        if(crystalMovingUnlockButton.unlock) canMove=true;
    }
    public void UnlockCrystalExplosion(){
        if(crystalExplosionUnlockButton.unlock) canExplode=true;
    }
    public void UnlockCrystalMultiCrystal(){
        if(crystalMutilCrystalUnlockButton.unlock) canUseMutilStacks=true;
    }
    private void UnlockCrystalSimple(){
        if(crystalSimpleUnlockButton.unlock) crystalSimpleUnlocked=true;
    }
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockCrystalMirage();
        UnlockCrystalMoving();
        UnlockCrystalExplosion();
        UnlockCrystalMultiCrystal();
        UnlockCrystalSimple();
    }
}
