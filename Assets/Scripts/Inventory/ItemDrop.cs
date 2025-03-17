using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private int possibleItemDrop;
    [SerializeField] private ItemData[] possibleDrop;
    private List<ItemData> dropList=new List<ItemData>();

    
    
    protected void DropItem(ItemData _itemdata){
        GameObject newItem=Instantiate(dropPrefab,transform.position,Quaternion.identity);
        Vector2 randomVelecity=new Vector2(Random.Range(-5f,5f),Random.Range(5f,15f));
        newItem.GetComponent<ItemObject>().SetUpItemDrop(_itemdata,randomVelecity);
    }
    public virtual void GenerateDropItem(){
        int j=0;
        while(j!=possibleItemDrop){
            int randomItem=Random.Range(0,100);
            for (int i = 0; i < possibleDrop.Length; i++)
            {
                if(randomItem<=possibleDrop[i].dropChance){
                    dropList.Add(possibleDrop[i]);
                    j++;
                    if(j==possibleItemDrop) break;
                    break;
                }
            }
        }
        for (int i = 0; i < possibleItemDrop; i++)
        {
            ItemData randomItem=dropList[Random.Range(0,dropList.Count-1)];
            DropItem(randomItem);
        }
            dropList.Clear();
    }
}
