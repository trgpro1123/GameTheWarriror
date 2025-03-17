using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private Transform craftParent;
    [SerializeField] private GameObject craftSlotPrefab;
    [SerializeField] private List<ItemData_Equipment> craftEquitment;

    private void Start() {
        transform.parent.GetChild(0).GetComponent<UI_CraftList>().SetUpCraftList();
        SetUpDefaultCraftWindow();
    }


    public void SetUpCraftList(){
        for (int i = 0; i < craftParent.childCount; i++)
        {
            Destroy(craftParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < craftEquitment.Count; i++)
        {
            GameObject newCraftSlot=Instantiate(craftSlotPrefab,craftParent);
            newCraftSlot.GetComponent<UI_CraftSlot>().SetUpCraftSlot(craftEquitment[i]);
        }
    }
    public void SetUpDefaultCraftWindow(){
        if(craftEquitment[0]!=null)
            GetComponentInParent<UI>().craftWindow.SetUpCraftWindow(craftEquitment[0]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }
}
