using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;
    [TextArea]
    public string descriptionStat;


    private UI ui;

    private void OnValidate() {
        gameObject.name="Stat - "+statName;
        if(statNameText!=null){
            statNameText.text=statName;
        }
    }

    private void Start() {
        UpdateStatValueUI();
        ui=GetComponentInParent<UI>();   
    }
    public void UpdateStatValueUI(){
        PlayerStats playerStats=PlayerManage.instance.player.GetComponent<PlayerStats>();
        if(playerStats!=null){
            statValueText.text=playerStats.GetType(statType).GetValue().ToString();
        }
        if(statType==StatType.health){
            statValueText.text=playerStats.GetMaxHealth().ToString();
        }
        if(statType==StatType.damage){
            statValueText.text=(playerStats.damage.GetValue()+playerStats.strength.GetValue()).ToString();

        }
        if(statType==StatType.evasion){
            statValueText.text=(playerStats.evasion.GetValue()+playerStats.agility.GetValue()).ToString();
        }
        if(statType==StatType.critChance){
            statValueText.text=(playerStats.critChance.GetValue()+playerStats.agility.GetValue()).ToString();
        }
        if(statType==StatType.critPower){
            statValueText.text=(playerStats.critPower.GetValue()+playerStats.strength.GetValue()).ToString();
        }
        if(statType==StatType.magicRes){
            statValueText.text=(playerStats.magicResistance.GetValue()+playerStats.intelligence.GetValue()*3).ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statTooltip.ShowStatTooltip(descriptionStat);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statTooltip.HideStatTooltip();
    }
}
