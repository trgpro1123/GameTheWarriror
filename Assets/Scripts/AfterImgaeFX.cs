using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImgaeFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private float colorLooseRate;

    public void SetUpAfterImage(Sprite _sprite,float _LooseRate){
        sr=GetComponent<SpriteRenderer>();
        sr.sprite=_sprite;
        colorLooseRate=_LooseRate;
    }
    private void Update() {
        float alpha=sr.color.a-colorLooseRate*Time.deltaTime;

        sr.color=new Color(sr.color.r,sr.color.g,sr.color.b,alpha);

        if(sr.color.a<=0) Destroy(gameObject);
        
    }
}
