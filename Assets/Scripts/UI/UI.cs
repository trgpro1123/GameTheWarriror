using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour,ISaveManager
{
    public UI_ItemTooltip itemTooltip;
    public UI_StatTooltip statTooltip;
    public UI_CraftWindow craftWindow;
    public UI_SkillTooltip skillTooltip;
    [SerializeField] private GameObject chacraterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject ingameUI;
    [Space]
    [SerializeField] private UI_FadeSceen fadeSceen;
    [SerializeField] private GameObject diedText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private UI_VolumeSlider[] volumeSliders;
    private void Awake() {
        SwitchTo(skillTreeUI);
        fadeSceen.gameObject.SetActive(true);
    }
    private void Start() {
        SwitchTo(ingameUI);
        itemTooltip.gameObject.SetActive(false);
        statTooltip.gameObject.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            SwitchWithKeyTo(chacraterUI);
        }
        if(Input.GetKeyDown(KeyCode.O)){
            SwitchWithKeyTo(optionsUI);
        }
        if(Input.GetKeyDown(KeyCode.C)){
            SwitchWithKeyTo(craftUI);
        }
        if(Input.GetKeyDown(KeyCode.K)){
            SwitchWithKeyTo(skillTreeUI);
        }
    }
   public void SwitchTo(GameObject _menu){

        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeSceen=transform.GetChild(i).GetComponent<UI_FadeSceen>()!=null;
            if(!fadeSceen)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if(_menu!=null){
            AudioManager.instance.PlaySFX(7);
            _menu.gameObject.SetActive(true);
        }

        if(GameManager.instance!=null){
            if(_menu==ingameUI){
                GameManager.instance.PauseGame(false);
            }else{
                GameManager.instance.PauseGame(true);
            }
        }
   }
   public void SwitchWithKeyTo(GameObject _menu){
        if(_menu!=null&&_menu.activeSelf){
            _menu.SetActive(false);
            CheckForInGame();
            return;
        }
        SwitchTo(_menu);
   }
   private void CheckForInGame(){
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf&&transform.GetChild(i).GetComponent<UI_FadeSceen>()==null)
                return;
            
        }
        SwitchTo(ingameUI);
    }
    public void SwitchOnEndScreen(){
        fadeSceen.FadeOut();
        StartCoroutine(EndScreenCoroutine());

    }
    private IEnumerator EndScreenCoroutine(){
        yield return new WaitForSeconds(1.5f);
        diedText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);

    }
    public void RestartGameButton()=>GameManager.instance.RestartScene();

    public void SaveData(ref GameData _data)
    {
        _data.audioSetting.Clear();
        foreach (UI_VolumeSlider item in volumeSliders)
        {
            _data.audioSetting.Add(item.parametr,item.slider.value);
        }
    }

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string,float> pair in _data.audioSetting)
            {
                foreach (UI_VolumeSlider item in volumeSliders)
                {
                    if(pair.Key==item.parametr){
                        item.LoadSlider(pair.Value);
                    }
            
                }
            }
        
    }
    public void SaveAndGoBackMainMenu(){
        SaveManager.instance.SaveGame();
        Application.Quit();
    }
}
