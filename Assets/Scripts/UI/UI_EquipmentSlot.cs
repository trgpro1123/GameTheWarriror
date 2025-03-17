using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;
    private void OnValidate() {
        gameObject.name="EquipMent - "+equipmentType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(inventoryItem==null||inventoryItem.itemData==null) return;
        Inventory.instance.UnequipmentItem(inventoryItem.itemData as ItemData_Equipment);
        Inventory.instance.AddItem(inventoryItem.itemData as ItemData_Equipment);
        ClearUpSlot();
        ui.itemTooltip.HideTooltip();
    }
}
