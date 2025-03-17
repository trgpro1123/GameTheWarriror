using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SkillTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillPrice;




    public void ShowSkillTreeTooltip(string _skillname,string _skillDescription,int _price){
        skillName.text=_skillname;
        skillDescription.text=_skillDescription;
        skillPrice.text="Giá: "+_price.ToString();
        AdjustPosition();
        gameObject.SetActive(true);
    }
    public void HideSkillTreeTooltip(){
        gameObject.SetActive(false);
    }
}
