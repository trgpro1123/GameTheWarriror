using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{

    protected override void Start()
    {
        base.Start();

    }
    
    public void SetUpCraftSlot(ItemData_Equipment _item){
        inventoryItem.itemData=_item;
        imageItem.sprite=_item.icon;
        itemText.text=_item.itemName;
        
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetUpCraftWindow(inventoryItem.itemData as ItemData_Equipment);
    }
}
