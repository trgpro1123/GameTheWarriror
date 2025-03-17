using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ItemTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private int textDefaultFontSize=35;
    public void ShowTooltip(ItemData_Equipment _item){
        if(_item!=null){
            AdjustPosition();
            itemNameText.text=_item.itemName;
            itemTypeText.text=_item.itemType.ToString();
            itemDescriptionText.text=_item.GetDesciptrion();

            if(itemNameText.text.Length>19){
                itemNameText.fontSize=itemNameText.fontSize*0.7f;
            }else{
                itemNameText.fontSize=textDefaultFontSize;
            }
            gameObject.SetActive(true);

        }
    }
    public void HideTooltip(){
        itemNameText.fontSize=textDefaultFontSize;
        gameObject.SetActive(false);
    }
}
