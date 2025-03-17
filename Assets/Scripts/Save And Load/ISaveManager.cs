using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager 
{
    public void SaveData(ref GameData _data);
    public void LoadData(GameData _data);
}
