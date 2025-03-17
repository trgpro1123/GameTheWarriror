using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    public int currency;
    public SerializableDictionary<string,int> inventory;
    public SerializableDictionary<string,bool> skillTree;
    public SerializableDictionary<string,bool> checkPoint;
    public SerializableDictionary<string,float> audioSetting;
    public List<string> equiment;
    public string checkPointClossest;
    public float lostcurrncyX;
    public float lostcurrncyY;
    public int lostcurrncyAmount;

    public GameData()
    {
        currency = 0;
        lostcurrncyX=0;
        lostcurrncyY=0;
        lostcurrncyAmount=0;
        inventory=new SerializableDictionary<string,int>();
        skillTree=new SerializableDictionary<string,bool>();
        checkPoint=new SerializableDictionary<string,bool>();
        audioSetting=new SerializableDictionary<string,float>();
        equiment=new List<string>();
        checkPointClossest=string.Empty;
    }
}
