using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    [SerializeField] private string dataFileName;
    // [SerializeField] private string filePath="idbfs/TheWarrirordhsdfe532";
    [SerializeField] private bool encryptData;
    private List<ISaveManager> saveManagers;
    private GameData gameData;

    private FileDataHandler dataHandler;



    [ContextMenu("Delete save file")]
    public void DeleteSaveData(){
        dataHandler=new FileDataHandler(Application.persistentDataPath,dataFileName,encryptData);
        dataHandler.Delete();
    }

    
    private void Awake() {
        if(instance==null) instance=this;
        else Destroy(instance.gameObject);
    }


    private void Start() {
        dataHandler=new FileDataHandler(Application.persistentDataPath,dataFileName,encryptData);
        saveManagers=FindAllFileSaveManagers();
        LoadGame();
    }
    public void SaveGame(){
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }
    public void LoadGame(){
        gameData=dataHandler.Load();
        
        if(this.gameData==null){
            newGame();
            Debug.Log("No data");
        }
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }


    }
    private void newGame(){
        gameData=new GameData();
    }

    private void OnApplicationQuit() {
       SaveGame();
    }
    private List<ISaveManager> FindAllFileSaveManagers(){
        IEnumerable<ISaveManager> saveManagers=FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }
    
}
