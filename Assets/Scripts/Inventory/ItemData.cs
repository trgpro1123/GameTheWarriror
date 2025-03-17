using UnityEngine;
using System.Text;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public enum ItemType{
    Material,
    Equipment
}
[CreateAssetMenu(fileName ="New Item Data",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    [Range(0,100)]
    public float dropChance;
    public string itemID;

    private void OnValidate() {
#if UNITY_EDITOR 
    string path=AssetDatabase.GetAssetPath(this);
    itemID=AssetDatabase.AssetPathToGUID(path);
#endif
    }
    protected StringBuilder sb=new StringBuilder();
    public virtual string GetDesciptrion(){
        return "";
    }
}
