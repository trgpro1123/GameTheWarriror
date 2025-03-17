using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerFX : EntityFX
{
    [Header("After Image FX")]
    private float afterImageTimer;
    [SerializeField] private float afterImgaeFXCooldown;
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colerLooseRate;

    [Header("Screen Shake FX")]
    private CinemachineImpulseSource shakeScreen;
    [SerializeField] private float shakeImpact;
    public Vector3 shakeHighDamaged;

    
    protected override void Start()
    {
        base.Start();
        shakeScreen=GetComponent<CinemachineImpulseSource>();
    }

    private void Update() {
        afterImageTimer-=Time.deltaTime;
    }
    public void CreateAfterImage(){
        if(afterImageTimer<0){
            afterImageTimer=afterImgaeFXCooldown;
            GameObject newAfterImage=Instantiate(afterImagePrefab,transform.position,transform.rotation);
            newAfterImage.GetComponent<AfterImgaeFX>().SetUpAfterImage(spriteRenderer.sprite,colerLooseRate);
        }
    }
    public void ScreenShake(Vector3 _shakePower){
        shakeScreen.m_DefaultVelocity=new Vector3(_shakePower.x*player.facingDir,_shakePower.y)*shakeImpact;
        shakeScreen.GenerateImpulse();
    }
    
}
