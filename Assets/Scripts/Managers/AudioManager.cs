using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private float sfxMinimunDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBMG;
    private int indexBMG;
    private bool canPlaySFX;

    private void Awake() {
        if(instance!=null) Destroy(instance.gameObject);
        else instance=this;
        Invoke("AllowPlaySFX",.1f);
    }
    private void Update() {
        if(!playBMG){
            StopAllBMG();
        }else{
            if(!bgm[indexBMG].isPlaying){
                PlayBMG(indexBMG);
            }
        }
    }

    public void PlaySFX(int _indexSFX,Transform _source=null){

        // if(sfx[_indexSFX].isPlaying) 
        //     return;
        if(canPlaySFX==false) return;
        if(_source!=null&&Vector2.Distance(PlayerManage.instance.player.transform.position,_source.position)>sfxMinimunDistance)
            return;
        sfx[_indexSFX].pitch=Random.Range(.85f,1.1f);
        if(_indexSFX<sfx.Length){
            sfx[_indexSFX].Play();
        }
    }
    public void StopSFX(int _indexSFX) => sfx[_indexSFX].Stop();

    public void PlayBMG(int _indexBMG){
        
        indexBMG=_indexBMG;
        StopAllBMG();
        if(_indexBMG<bgm.Length){
            bgm[indexBMG].Play();
        }
    }
    public void StopAllBMG(){
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    private void PlayRandomBMG(){
        indexBMG=Random.Range(0,bgm.Length);
        bgm[indexBMG].Play();
    }
    private void AllowPlaySFX()=>canPlaySFX=true;
}
