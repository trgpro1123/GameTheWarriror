using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected Image imageItem;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI ui;

    public InventoryItem inventoryItem;

    protected virtual void Start() {
        ui=GetComponentInParent<UI>();
    }
    public void UpdateSlot(InventoryItem _invetory){
        inventoryItem=_invetory;
        imageItem.color=Color.white;
        if(inventoryItem!=null){
            imageItem.sprite=inventoryItem.itemData.icon;
            if(inventoryItem.stackSize>1){
                itemText.text=inventoryItem.stackSize.ToString();
            }else{
                itemText.text="";
            }
        }
    }
    public void ClearUpSlot(){
        inventoryItem=null;
        imageItem.sprite=null;
        imageItem.color=Color.clear;
        itemText.text="";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(inventoryItem==null) return;
        if(Input.GetKey(KeyCode.LeftControl)){
            Inventory.instance.RemoveItem(inventoryItem.itemData);
            return;
        }
        if(inventoryItem.itemData.itemType==ItemType.Equipment)
            Inventory.instance.EquipItem(inventoryItem.itemData);

        ui.itemTooltip.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(inventoryItem==null) return;
        ui.itemTooltip.ShowTooltip(inventoryItem.itemData as ItemData_Equipment);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(inventoryItem==null) return;
        ui.itemTooltip.HideTooltip();
    }
}
