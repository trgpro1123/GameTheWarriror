using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SkillTreeSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISaveManager
{
    public bool unlock;
    [SerializeField] private string skillName;
    [SerializeField] private int skillPrice;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color defaultImageColor;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlock;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLock;

    private Image image;
    private UI ui;
    


    private void OnValidate() {
        gameObject.name="SkillTree_UI - "+skillName;
    }
    private void Awake() {
        
        GetComponent<Button>().onClick.AddListener(()=> UnlockSkillSLot());

    }
    private void Start() {
        image=GetComponent<Image>();
        ui=GetComponentInParent<UI>();

        image.color=defaultImageColor;
        if(unlock)
            image.color=Color.white;

    }

    public void UnlockSkillSLot(){
        if(PlayerManage.instance.HaveEnoughMoney(skillPrice)==false) return;
        for (int i = 0; i < shouldBeUnlock.Length; i++)
        {
            if(shouldBeUnlock[i].unlock==false) return;
        }
        for (int i = 0; i < shouldBeLock.Length; i++)
        {
            if(shouldBeLock[i].unlock==true) return;
        }
        unlock=true;
        image.color=Color.white;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillTooltip.ShowSkillTreeTooltip(skillName,skillDescription,skillPrice);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.HideSkillTreeTooltip();
    }

    public void SaveData(ref GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value)){

            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName,unlock);
        }
        else
            _data.skillTree.Add(skillName,unlock);
    }

    public void LoadData(GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName,out bool value)){
            unlock=value;
        }
    }
    
}
