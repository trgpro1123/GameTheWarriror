using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_StatTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI statName;


    

    

    public void ShowStatTooltip(string descriptionStat){
        
        statName.text=descriptionStat;
        AdjustPosition();
        gameObject.SetActive(true);
    }
    public void HideStatTooltip(){
        
        gameObject.SetActive(false);
    }
}
