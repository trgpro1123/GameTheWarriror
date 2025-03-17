using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity=>GetComponentInParent<Entity>();
    private CharaterStats charaterStats=>GetComponentInParent<CharaterStats>();
    private RectTransform rectTransform;
    private Slider slider;




    private void Start() {

        rectTransform=GetComponent<RectTransform>();
        slider=GetComponentInChildren<Slider>();


        
        UpdateHealth();

    }
    

    public void UpdateHealth(){
        slider.maxValue=charaterStats.GetMaxHealth();
        slider.value=charaterStats.currentHealth;

    }

    private void FlipUI(){
        rectTransform.Rotate(0,180,0);
    }
    private void OnEnable() {
        entity.onFlipped+=FlipUI;
        charaterStats.onHealthChanged+=UpdateHealth;
        
    }


    private void OnDisable() {
        if(entity!=null)
            entity.onFlipped-=FlipUI;
        if(charaterStats!=null)
            charaterStats.onHealthChanged-=UpdateHealth;
    }

}
