using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Ingame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider healbarTopLeft;

    [Header("Soul info")]
    [SerializeField] private TextMeshProUGUI currency;
    [SerializeField] private float soulAmount;
    [SerializeField] private float increaserate;

    [Header("Image")]
    [SerializeField] private Image parryImage;
    [SerializeField] private Image attackImage;
    [SerializeField] private Image dashImage;
    [SerializeField] private Image crystaImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskImage;
    private SkillManager skill;

    private void Start() {
        skill=SkillManager.instance;
        if(playerStats!=null){
            playerStats.onHealthChanged+=UpdateHealth;
        }
    }
    private void Update()
    {
        UpdateSoulUI();

        if (Input.GetKeyDown(KeyCode.Q) && skill.parry.parryUnlocked)
        {
            SetCoolDownOf(parryImage);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dash.dashUnlocked)
        {
            SetCoolDownOf(dashImage);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && skill.sword.swordUnlocked)
        {
            SetCoolDownOf(attackImage);
        }
        if (Input.GetKeyDown(KeyCode.F) && skill.crystal.crystalSimpleUnlocked)
        {
            SetCoolDownOf(crystaImage);
        }
        if (Input.GetKeyDown(KeyCode.E) && skill.blackhole.blackholeUnlocked)
        {
            SetCoolDownOf(blackholeImage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquiment(EquipmentType.Flask) != null)
        {
            SetCoolDownOf(flaskImage);
        }

        CheckCoolDownOf(parryImage, skill.parry.cooldown);
        CheckCoolDownOf(attackImage, skill.sword.cooldown);
        CheckCoolDownOf(dashImage, skill.dash.cooldown);
        CheckCoolDownOf(crystaImage, skill.crystal.cooldown);
        CheckCoolDownOf(blackholeImage, skill.blackhole.cooldown);
        CheckCoolDownOf(flaskImage, Inventory.instance.flaskCooldown);

    }

    private void UpdateSoulUI()
    {
        if (soulAmount < PlayerManage.instance.GetCurrency())
        {
            soulAmount += increaserate * Time.deltaTime;
        }
        else
        {
            soulAmount = PlayerManage.instance.GetCurrency();
        }
        currency.text = ((int)soulAmount).ToString("#,#");
    }

    private void UpdateHealth(){
        healbarTopLeft.maxValue=playerStats.GetMaxHealth();
        healbarTopLeft.value=playerStats.currentHealth;

    }
    private void SetCoolDownOf(Image _image){
        if(_image.fillAmount<=0)
            _image.fillAmount=1;
    }
    private void CheckCoolDownOf(Image _image,float _cooldown){
        if(_image.fillAmount>0)
            _image.fillAmount-=(1/_cooldown)*Time.deltaTime;
    }
    
}
