using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad="Main Scene";
    [SerializeField] private GameData continueButton;
    [SerializeField] private UI_FadeSceen fadeSceen;
    [SerializeField] private GameObject howToPlay;

    public void Continue(){
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void NewGame(){
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void ExitGame(){
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay){
        fadeSceen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneNameToLoad);
    }
    public void HowToPlay(){
        if(howToPlay.activeSelf==true){
            howToPlay.SetActive(false);
        }
        else{
            howToPlay.SetActive(true);
        }
    }
}
