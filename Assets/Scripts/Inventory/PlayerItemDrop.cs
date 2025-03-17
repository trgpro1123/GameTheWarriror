using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drop")]
    [SerializeField][Range(0,100)] private int equimentDropChance;
    [SerializeField][Range(0,100)] private int stashDropChance;
    public override void GenerateDropItem()
    {
        Inventory inventory=Inventory.instance;
        List<InventoryItem> itemsToDrop=new List<InventoryItem>();
        List<InventoryItem> materialsToDrop=new List<InventoryItem>();

        foreach (InventoryItem item in inventory.GetEquimetList)
        {
            if(Random.Range(0,100)<=equimentDropChance){
                itemsToDrop.Add(item);
                DropItem(item.itemData);
            }
        }
        for (int i = 0; i < itemsToDrop.Count; i++)
        {
            inventory.UnequipmentItem(itemsToDrop[i].itemData as ItemData_Equipment);
        }
        foreach (InventoryItem item in inventory.GetStashList)
        {
            if(Random.Range(0,100)<=stashDropChance){
                materialsToDrop.Add(item);
                DropItem(item.itemData);

            }
        }
        for (int i = 0; i < materialsToDrop.Count; i++)
        {
            DropItem(materialsToDrop[i].itemData);
        }

    }
}
