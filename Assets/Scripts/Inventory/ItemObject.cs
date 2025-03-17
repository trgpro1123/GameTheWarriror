using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;

    private void SetUpVisual(){
        if(itemData==null) return;
        else{
            GetComponent<SpriteRenderer>().sprite=itemData.icon;
            gameObject.name="Item - "+itemData.itemName;
        }
    }
    public void SetUpItemDrop(ItemData _itemData,Vector2 _velocity){
        rb.velocity=_velocity;
        itemData=_itemData;

        SetUpVisual();
    }


    public void PickUpItem()
    {
        if(Inventory.instance.CanAddItem()&&itemData.itemType==ItemType.Equipment){
            rb.velocity=new Vector2(0,7);
            PlayerManage.instance.player.fX.CreatePopUpText("inventory full!");
            return;
        }
        AudioManager.instance.PlaySFX(18);
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);

    }
}
