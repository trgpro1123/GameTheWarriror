using System;

[Serializable]
public class InventoryItem 
{
    public ItemData itemData;
    public int stackSize;
    public InventoryItem(ItemData _data){
        itemData=_data;
        AddStack();
    }
    public void AddStack()=>stackSize++;
    public void RemoveStack()=>stackSize--;
}
