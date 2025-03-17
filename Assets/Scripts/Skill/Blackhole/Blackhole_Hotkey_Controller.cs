using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blackhole_Hotkey_Controller : MonoBehaviour
{
    private KeyCode keyCode;
    private SpriteRenderer sp;
    private TextMeshProUGUI textHotKey;
    private Transform enemyTranform;
    Blackhole_Skill_Controller blackhole_Skill_Controller;

    void Update()
    {
        if(Input.GetKeyDown(keyCode)){
            blackhole_Skill_Controller.AddEnemyToList(enemyTranform);
            textHotKey.color=Color.clear;
            sp.color=Color.clear;
        }
            
    }
    public void SetUpHotkey(KeyCode _keyCode,Transform _enemyTranform,Blackhole_Skill_Controller _skill_Controller){
        sp=GetComponent<SpriteRenderer>();
        textHotKey=GetComponentInChildren<TextMeshProUGUI>();

        enemyTranform=_enemyTranform;
        blackhole_Skill_Controller=_skill_Controller;
        keyCode=_keyCode;
        textHotKey.text=_keyCode.ToString();
    }
}
