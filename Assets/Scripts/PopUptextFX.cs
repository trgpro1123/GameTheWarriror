using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUptextFX : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float colorLooseSpeed;
    [SerializeField] private float speedDesappear;
    [SerializeField] private float lifeTime;

    private TextMeshPro popUpText;
    private float textTimer;

    private void Start() {
        popUpText=GetComponent<TextMeshPro>();
        textTimer=lifeTime;
    }
    private void Update() {
        textTimer-=Time.deltaTime;
        transform.position=Vector2.MoveTowards(transform.position,
        new Vector2(transform.position.x,transform.position.y+1),speed*Time.deltaTime);

        if(textTimer<0){
            float alpha=popUpText.color.a-colorLooseSpeed*Time.deltaTime;
            popUpText.color=new Color(popUpText.color.r,popUpText.color.g,popUpText.color.b,alpha);
            if(popUpText.color.a<50){
                speed=speedDesappear;

            }
            if(popUpText.color.a<=0) Destroy(gameObject);
        }
    }
}
