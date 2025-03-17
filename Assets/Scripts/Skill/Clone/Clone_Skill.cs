using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clone_Skill : Skill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject clonePlayerPrefab;
    [SerializeField] private UI_SkillTreeSlot canAttackCloneUnlockButton;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;
    [SerializeField] private float damageCanAttackCloneSimple;


    [Header("Agressive Mirage")]
    [SerializeField] private UI_SkillTreeSlot agressiveMirageUnlockButton;
    [SerializeField] private float damageCloneAgressiveMirage;
    public bool agressiveMirageUnlocked;

    // [SerializeField] private bool canCreateCloneOnStart;
    // [SerializeField] private bool canCreateCloneOnOver;
    // [SerializeField] private bool canCreateCloneOnCounterAttack;
    [Header("Multiple Mirage")]
    [SerializeField] private UI_SkillTreeSlot mutipleMirageUnlockButton;
    [SerializeField] [Range(0,100)] private int changeToDuplicate;
    [SerializeField] private float damageMultipleMirage;
    public bool canDuplicateClone{get;private set;}


    [Header("Crystal - Mirage")]
    [SerializeField] private UI_SkillTreeSlot crystalMirageUnlockButton;
    public bool crystalInsteadOfClone{get;private set;}


    private float attackMultiplier;


    protected override void Start()
    {
        base.Start();
        canAttackCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCanAttackClone);
        agressiveMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAgressiveMirage);
        mutipleMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMutialMirage);
        crystalMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInstendOfClone);

    }
    #region Unlock Region
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockCanAttackClone();
        UnlockAgressiveMirage();
        UnlockMutialMirage();
        UnlockCrystalInstendOfClone();
    }
    public void UnlockCanAttackClone(){
        if(canAttackCloneUnlockButton.unlock){
            attackMultiplier=damageCanAttackCloneSimple;
            canAttack=true;
        }
    }
    public void UnlockAgressiveMirage(){
        if(agressiveMirageUnlockButton.unlock){
            attackMultiplier=damageCloneAgressiveMirage;
            agressiveMirageUnlocked=true;
        }
    }
    public void UnlockMutialMirage(){
        if(mutipleMirageUnlockButton.unlock){
            attackMultiplier=damageMultipleMirage;
            canDuplicateClone=true;
        }
    }
    public void UnlockCrystalInstendOfClone(){
        if(crystalMirageUnlockButton.unlock) crystalInsteadOfClone=true;
    }

    #endregion

    public void CreateClone(Transform _clonePosition,Vector3 _offset){
        if(crystalInsteadOfClone){
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }
        GameObject clone=Instantiate(clonePlayerPrefab);
        clone.GetComponent<Clone_Skill_Controller>().SetUpClone(_clonePosition,cloneDuration,canAttack,_offset,canDuplicateClone,changeToDuplicate,FindClosestEnemy(_clonePosition),player,attackMultiplier);
    }
    
    public void CreateCloneWithDelay(Transform _enemyTransform){
        StartCoroutine(CloneDelayCoroutine(_enemyTransform,new Vector3(2*player.facingDir,0)));
    }
    public IEnumerator CloneDelayCoroutine(Transform _transform,Vector3 _offset){
        yield return new WaitForSeconds(0.3f);
        CreateClone(_transform,_offset);
    }

}
