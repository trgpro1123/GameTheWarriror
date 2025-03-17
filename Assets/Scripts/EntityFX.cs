using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class EntityFX : MonoBehaviour
{
    [Header("FX")]
    [SerializeField] private float flashDuration=0.2f;
    [SerializeField] private Material flashMaterial;

    [SerializeField] private Color[] ignitedColor;
    [SerializeField] private Color[] chilledColor;
    [SerializeField] private Color[] shockedColor;

    [Header("Aliment Particle")]
    [SerializeField] private ParticleSystem igniteFX;
    [SerializeField] private ParticleSystem chillFX;
    [SerializeField] private ParticleSystem shockFX;

    [Header("Hit FX")]
    [SerializeField] private GameObject hitFXPrefab;
    [SerializeField] private GameObject critHitFXPrefab;

    

    

    [Header("Pop up text FX")]
    [SerializeField] private GameObject popUpTextFXPrefab;

    


    private Material originMaterial;
    protected SpriteRenderer spriteRenderer;
    protected Player player;

    private GameObject myHealthBar;
    protected virtual void Start()
    {
        player=PlayerManage.instance.player;
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
        originMaterial=spriteRenderer.material;
        myHealthBar=GetComponentInChildren<HealthBar_UI>().gameObject;
        
    }
    
    
    
    public void CreatePopUpText(string _text){
        float xOffset=Random.Range(-1,1);
        float yOffset=Random.Range(1,2);
        Vector3 randomPositoon=new Vector3(xOffset,yOffset,0);

        GameObject newPopUpText=Instantiate(popUpTextFXPrefab,transform.position+randomPositoon,Quaternion.identity);
        newPopUpText.GetComponent<TextMeshPro>().text=_text;

    }
    


    private IEnumerator FlashFX(){
        spriteRenderer.material=flashMaterial;
        Color currentColor=spriteRenderer.color;
        spriteRenderer.color=Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material=originMaterial;
        spriteRenderer.color=currentColor;
    }
    private void CanncelColorChange(){
        CancelInvoke();
        spriteRenderer.color=Color.white;
        igniteFX.Stop();
        chillFX.Stop();
        shockFX.Stop();

    }
    private void RedColorBlink(){
        if(spriteRenderer.color!=Color.white) spriteRenderer.color=Color.white;
        else spriteRenderer.color=Color.red;
    }

    public void IgniteFXFor(float _time){
        igniteFX.Play();
        InvokeRepeating("IgniteFXColor",0,0.3f);
        Invoke("CanncelColorChange",_time);

    }
    private void IgniteFXColor(){
        if(spriteRenderer.color!=ignitedColor[0]) spriteRenderer.color=ignitedColor[0];
        else spriteRenderer.color=ignitedColor[1];
    }
    
    public void ChillFXFor(float _time){
        chillFX.Play();
        InvokeRepeating("ChillFXColor",0,0.3f);
        Invoke("CanncelColorChange",_time);

    }
    private void ChillFXColor(){
        if(spriteRenderer.color!=chilledColor[0]) spriteRenderer.color=chilledColor[0];
        else spriteRenderer.color=chilledColor[1];
    }

    public void ShockFXFor(float _time){
        shockFX.Play();
        InvokeRepeating("ShockFXColor",0,0.3f);
        Invoke("CanncelColorChange",_time);

    }
    private void ShockFXColor(){
        if(spriteRenderer.color!=shockedColor[0]) spriteRenderer.color=shockedColor[0];
        else spriteRenderer.color=shockedColor[1];
    }
    public void MakeTransprent(bool _transprent){
        if(_transprent){
            myHealthBar.SetActive(false);
            spriteRenderer.color=Color.clear;
        }
        else{
            myHealthBar.SetActive(true);
            spriteRenderer.color=Color.white;
        }
    }
    public void CreateHitFX(Transform _target,bool _isCrit){

        float xPosition=Random.Range(-.5f,.5f);
        float yPosition=Random.Range(-.5f,.5f);
        float zRoutation=Random.Range(-90,90);

        GameObject hitFX=hitFXPrefab;
        Vector3 hitFXRoutation=new Vector3(0,0,zRoutation);

        if(_isCrit){
            hitFX=critHitFXPrefab;
            float yRoutation=0;
            zRoutation=Random.Range(-45,45);
            if(GetComponent<Entity>().facingDir==-1)
                yRoutation=180;
            
            hitFXRoutation=new Vector3(0,yRoutation,zRoutation);

        }

        GameObject newHitFX=Instantiate(hitFX,_target.position+new Vector3(xPosition,yPosition),Quaternion.identity);
        newHitFX.transform.Rotate(hitFXRoutation);
        Destroy(newHitFX,0.5f);


    }

    
}
