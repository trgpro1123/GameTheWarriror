using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SwordSkillType{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordSkillType swordSkillType=SwordSkillType.Regular;
    

    [Header("Skill Sword Info")]
    [SerializeField] private UI_SkillTreeSlot swordUnlockButton;
    public bool swordUnlocked;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float timeFreezeDuration;
    [SerializeField] private float timeDestroySword;

    [Header("Aim Info")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int numberDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private Transform dotParent;

    private GameObject[] dots;
    private Vector2 finalDir;

    [Header("Bounce Info")]
    [SerializeField] private UI_SkillTreeSlot swordBounceUnlockButton;
    public bool swordBounceUnlocked;
    [SerializeField] private int bouncingAmount=5;
    [SerializeField] private float speedBoucing=10f;
    [SerializeField] private float bounceGravity=4f;

    [Header("Pierce Info")]
    [SerializeField] private UI_SkillTreeSlot swordPierceUnlockButton;
    public bool swordPierceUnlocked;
    [SerializeField] private int pierceAmount=3;
    [SerializeField] private float pierceGravity=1f;

    [Header("Spin Info")]
    [SerializeField] private UI_SkillTreeSlot swordSpinUnlockeButton;
    public bool swordSpinUnlocked;
    [SerializeField] private float hitCooldown;
    [SerializeField] private float maxTravelDistance;
    [SerializeField] private float spinDuration;
    [SerializeField] private float spinGravity;


    [Header("Passive Skill")]
    [SerializeField] private UI_SkillTreeSlot timeStopUnlockButton;
    public bool timeStopUnlocked;
    [SerializeField] UI_SkillTreeSlot timeStopVunerabilityUnlockButton;
    public bool timeStopVunerabilityUnlocked;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
        SetUpGravity();
        swordUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSword);
        swordBounceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordBounce);
        swordPierceUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordPierce);
        swordSpinUnlockeButton.GetComponent<Button>().onClick.AddListener(UnlockSwordSpin);
        timeStopUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockTimeStop);
        timeStopVunerabilityUnlockButton.GetComponent<Button>().onClick.AddListener(UnlocktimeStopVunerability);

    }
    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyUp(KeyCode.Mouse1)) 
            finalDir=new Vector2(AimDirection().normalized.x*launchForce.x,AimDirection().normalized.y*launchForce.y);
        if(Input.GetKey(KeyCode.Mouse1)){
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position=DotsPosition(i*spaceBeetwenDots);
            }
        }
        
    }
    #region Unlock Region
    protected override void CheckUnlock()
    {
        base.CheckUnlock();
        UnlockSword();
        UnlockSwordBounce();
        UnlockSwordPierce();
        UnlockSwordSpin();
        UnlockTimeStop();
        UnlocktimeStopVunerability();
    }
    public void UnlockSword(){
        if(swordUnlockButton.unlock) {
            swordSkillType=SwordSkillType.Regular;
            swordUnlocked=true;
        }
        
    }
    public void UnlockSwordBounce(){
        if(swordBounceUnlockButton.unlock){
            swordSkillType=SwordSkillType.Bounce;
            swordBounceUnlocked=true;
        }
    }
    public void UnlockSwordPierce(){
        if(swordPierceUnlockButton.unlock){
            swordSkillType=SwordSkillType.Pierce;
            swordPierceUnlocked=true;
        }
    }
    public void UnlockSwordSpin(){
        if(swordSpinUnlockeButton.unlock){
            swordSkillType=SwordSkillType.Spin;
            swordSpinUnlocked=true;
        }
    }
    public void UnlockTimeStop(){
        if(timeStopUnlockButton.unlock) timeStopUnlocked=true;
    }
    public void UnlocktimeStopVunerability(){
        if(timeStopVunerabilityUnlockButton.unlock) timeStopVunerabilityUnlocked=true;
    }
    #endregion

    private void SetUpGravity(){
        if(swordSkillType==SwordSkillType.Bounce){
            swordGravity=bounceGravity;
        }else if(swordSkillType==SwordSkillType.Pierce){
            swordGravity=pierceGravity;
        }else if(swordSkillType==SwordSkillType.Spin){
            swordGravity=spinGravity;
        }
    }
    public void CreateSword(){
        GameObject newSword=Instantiate(swordPrefab,player.transform.position,transform.rotation);
        Sword_Skill_Controller skill_Controller=newSword.GetComponent<Sword_Skill_Controller>();
        if(swordSkillType==SwordSkillType.Bounce){
            skill_Controller.SetUpBouncingSkill(true,bouncingAmount,speedBoucing,bounceGravity);
        }else if(swordSkillType==SwordSkillType.Pierce){
            skill_Controller.SetUpPiercingSkill(pierceAmount);
        }else if(swordSkillType==SwordSkillType.Spin){
            skill_Controller.SetUpSpinningSkill(true,maxTravelDistance,spinDuration,spinGravity,hitCooldown);
        }
        skill_Controller.SetupSword(finalDir,swordGravity,player,returnSpeed,timeFreezeDuration,timeDestroySword);
        player.AssignNewSwrod(newSword);
        DotsActive(false);
    }



    public Vector2 AimDirection(){
        Vector2 playerPosition=player.transform.position;
        Vector2 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction=mousePosition-playerPosition;
        return direction;

    }
    public void DotsActive(bool _isActive){
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
    private void GenerateDots(){
        dots=new GameObject[numberDots];
        for (int i = 0; i < numberDots; i++)
        {
            dots[i]=Instantiate(dotPrefab,player.transform.position,Quaternion.identity,dotParent);
            dots[i].SetActive(false);
        }
    }
    private Vector2 DotsPosition(float t){

        Vector2 position=(Vector2)player.transform.position+new Vector2(
        AimDirection().normalized.x*launchForce.x,
        AimDirection().normalized.y*launchForce.y)*t +.5f*(Physics2D.gravity*swordGravity)*(t*t);
        return position;
        
    }
    



}
