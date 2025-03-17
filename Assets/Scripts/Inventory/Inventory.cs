using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Inventory : MonoBehaviour,ISaveManager
{
    public static Inventory instance;

    public List<ItemData> startingItem;
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_Equipment,InventoryItem> equipmentDictionary;
    public List<InventoryItem> inventory;
    public Dictionary<ItemData,InventoryItem> inventoryDictionary;
    public List<InventoryItem> stash;
    public Dictionary<ItemData,InventoryItem> stashDictionary;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipSlotParent;
    [SerializeField] private Transform statSlotParent;
    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipItemSlot;
    private UI_StatSlot[] statSlot;

    
    [Header("Items Cooldown")]
    private float lastTimeUseFlask;
    public float flaskCooldown{get;private set;}
    private float lastTimeUseArmor;
    private float armoCooldown;


    [Header("Data base")]
    public List<ItemData> itemDataBase;
    public List<InventoryItem> loadItems;
    public List<ItemData_Equipment> loadedEquiment;



    private void Awake() {
        if(instance==null) instance=this;
        else Destroy(gameObject);



        
        statSlot=statSlotParent.GetComponentsInChildren<UI_StatSlot>();
    }
 
    private void Start() {
        inventory=new List<InventoryItem>();
        stash=new List<InventoryItem>();
        equipment=new List<InventoryItem>();
        inventoryDictionary=new Dictionary<ItemData, InventoryItem>();
        stashDictionary=new Dictionary<ItemData, InventoryItem>();
        equipmentDictionary=new Dictionary<ItemData_Equipment, InventoryItem>();
        inventoryItemSlot=inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        stashItemSlot=stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipItemSlot=equipSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        // statSlot=statSlotParent.GetComponentsInChildren<UI_StatSlot>();
        AddStartingItem();

    }
    public void AddStartingItem(){
        if(loadedEquiment.Count>0){
            foreach (ItemData_Equipment item in loadedEquiment)
            {
                EquipItem(item);
            }
        }
        if(loadItems.Count>0){
            foreach (InventoryItem item in loadItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.itemData);
                }
            }
            return;
        }
        for (int i = 0; i < startingItem.Count; i++)
        {
            AddItem(startingItem[i]);
        }
    }
    public void EquipItem(ItemData _item){
        ItemData_Equipment newEquipItem=_item as ItemData_Equipment;
        InventoryItem newItem=new InventoryItem(newEquipItem);

        ItemData_Equipment oldItem=null;
        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equipmentType==newEquipItem.equipmentType){
                oldItem=item.Key;
            }
        }
        if(oldItem!=null)
        {
            UnequipmentItem(oldItem);
            AddToInventory(oldItem);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipItem,newItem);
        RemoveItem(_item);
        newEquipItem.AddModifiers();
        UpdateSlotUI();
    }

    public void UnequipmentItem(ItemData_Equipment _oldItem)
    {
        if (equipmentDictionary.TryGetValue(_oldItem, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(_oldItem);
            _oldItem.RemoveModifiers();
        }
        UpdateSlotUI();
    }

    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment&&!CanAddItem()) AddToInventory(_item);
        else AddToStash(_item);
  
        UpdateSlotUI();
    }
    public bool CanAddItem(){
        if(inventory.Count>=inventoryItemSlot.Length) return true;
        return false;
    }
    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }
    private void AddToStash(ItemData _item)
    {
        if (stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }
    }


    public void RemoveItem(ItemData _item){
        if(inventoryDictionary.TryGetValue(_item,out InventoryItem value)){
            if(value.stackSize<=1){
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else{
                value.RemoveStack();
            }
        }
        if(stashDictionary.TryGetValue(_item,out InventoryItem stashValue)){
            if(stashValue.stackSize<=1){
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else{
                stashValue.RemoveStack();
            }
        }
        UpdateSlotUI();
    }


    public void UpdateSlotUI()
    {
        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].ClearUpSlot();
        }
        for (int i = 0; i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].ClearUpSlot();
        }
        for (int i = 0; i < equipItemSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipItemSlot[i].equipmentType)
                {
                    equipItemSlot[i].UpdateSlot(item.Value);
                }
            }
        }


        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashItemSlot[i].UpdateSlot(stash[i]);
        }
        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        for (int i = 0; i < statSlot.Length; i++)
        {
            statSlot[i].UpdateStatValueUI();
        }
    }

    public bool CanCraft(ItemData_Equipment _itemToCraft, List<InventoryItem> _requiredMaterials ){
        List<InventoryItem> materialsToRemove=new List<InventoryItem>();
        for(int i=0;i<_requiredMaterials.Count;i++){
            if(stashDictionary.TryGetValue(_requiredMaterials[i].itemData,out InventoryItem stashValue)){
                if(stashValue.stackSize<_requiredMaterials[i].stackSize){
                    Debug.Log("Not Enought Materials");
                    return false;
                }else{
                    InventoryItem itemRemove=new InventoryItem(stashValue.itemData);
                    itemRemove.stackSize=_requiredMaterials[i].stackSize;
                    materialsToRemove.Add(itemRemove);
                }
            }else{
                Debug.Log("Not Enought Materials");
                return false;
            }
        }

        foreach (var materialToRemove in materialsToRemove)
        {
            for (int i = 0; i < materialToRemove.stackSize; i++)
            {
                RemoveItem(materialToRemove.itemData);
            }
        }

        AddItem(_itemToCraft);
        Debug.Log("Craft successful here your item "+_itemToCraft.name);

        return true;
    }
    public List<InventoryItem> GetEquimetList => equipment;
    public List<InventoryItem> GetStashList => stash;
    public ItemData_Equipment GetEquiment(EquipmentType _type){
        ItemData_Equipment equimentItem=null;
        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equipmentType==_type){
                equimentItem=item.Key;
            }
        }
        return equimentItem;
    }
    public void CanUseFlask(){
        ItemData_Equipment currentFlask=GetEquiment(EquipmentType.Flask);
        if(currentFlask==null) return;
        bool canUseFlask=Time.time>lastTimeUseFlask+flaskCooldown;
        if(canUseFlask){
            currentFlask.ItemEffect(null);
            flaskCooldown=currentFlask.itemCooldown;
            lastTimeUseFlask=Time.time;
            UpdateStatsUI();
        }
    }
    public bool CanUseArmo(){
        ItemData_Equipment currnetArmo=GetEquiment(EquipmentType.Armo);
        if(Time.time>lastTimeUseArmor+armoCooldown){
            lastTimeUseArmor=Time.time;
            armoCooldown=currnetArmo.itemCooldown;
            return true;
        }
        return false;
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equiment.Clear();
        foreach (KeyValuePair<ItemData,InventoryItem> pair in inventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemID,pair.Value.stackSize);
        }
        foreach (KeyValuePair<ItemData,InventoryItem> pair in stashDictionary)
        {
            _data.inventory.Add(pair.Key.itemID,pair.Value.stackSize);
        }
        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> pair in equipmentDictionary)
        {
            _data.equiment.Add(pair.Key.itemID);
        }
    }

    public void LoadData(GameData _data)
    {

        foreach (KeyValuePair<string,int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                if(item!=null&&item.itemID==pair.Key ){
                    InventoryItem itemToLoad=new InventoryItem(item);
                    itemToLoad.stackSize=pair.Value;
                    loadItems.Add(itemToLoad);
                }
            }
        }
        foreach (string loadedItemID in _data.equiment)
        {
            foreach (var item in itemDataBase)
            {
                if(item!=null&&item.itemID==loadedItemID){
                    loadedEquiment.Add(item as ItemData_Equipment);
                }
            }
        }
    }

#if UNITY_EDITOR 
    [ContextMenu("Fill up item DataBase")]
    private void FillUpItemDataBase()=> itemDataBase=new List<ItemData>(GetItemDataBase());

    private List<ItemData> GetItemDataBase(){
        string[] asstetNames=AssetDatabase.FindAssets("",new[]{"Assets/Data/Items"});
        List<ItemData> itemDatabase=new List<ItemData>();
        foreach (string SOName in asstetNames)
        {
            var SOPath=AssetDatabase.GUIDToAssetPath(SOName);
            var itemData=AssetDatabase.LoadAssetAtPath<ItemData>(SOPath);
            itemDatabase.Add(itemData);
        }
        return itemDatabase;
    }

#endif
}
