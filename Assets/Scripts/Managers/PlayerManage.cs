using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManage : MonoBehaviour,ISaveManager
{
    public static PlayerManage instance;
    public Player player;
    public int currency;
    private void Awake() {
        if(instance!=null) Destroy(instance.gameObject);
        else instance=this;
    }
    public bool HaveEnoughMoney(int _price){
        if(_price>currency) return false;

        currency-=_price;
        return true;
    }
    public int GetCurrency()=>currency;

    public void SaveData(ref GameData _data)
    {
        _data.currency=this.currency;
    }

    public void LoadData(GameData _data)
    {
        this.currency=_data.currency;  
    }
}
