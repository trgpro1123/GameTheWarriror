using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;



    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private string checkPointClosestId;

    [Header("Lost currency")]
    [SerializeField] private GameObject lostCurrencyPrefab;
    public int lostCurrencyAmount;
    private float lostcurrncyX;
    private float lostcurrncyY;


    private Transform player;

    private void Awake() {
        if(instance!=null) Destroy(instance.gameObject);
        else instance=this;
    }
    private void Start() {
        checkPoints=FindObjectsOfType<CheckPoint>();
        player=PlayerManage.instance.player.transform;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.X)){
            RestartScene();
        }
    }
    public void RestartScene(){
        SaveManager.instance.SaveGame();
        Scene scene=SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void SaveData(ref GameData _data)
    {
        _data.lostcurrncyX=player.position.x;
        _data.lostcurrncyY=player.position.y;
        _data.lostcurrncyAmount=lostCurrencyAmount;
        if(FindCheckPointClosest()!=null)
            _data.checkPointClossest=FindCheckPointClosest().Id;
        _data.checkPoint.Clear();
        foreach (CheckPoint checkPoint in checkPoints)
        {
            _data.checkPoint.Add(checkPoint.Id,checkPoint.actived);
        }
        
    }

    public void LoadData(GameData _data)
    {
        
        StartCoroutine(LoadWithDelay(_data));
    }

    private void LoadCheckPoint(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkPoint)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (pair.Key == checkPoint.Id && pair.Value == true)
                {
                    checkPoint.ActiveCheckPoint();
                }
            }
        }
    }

    private void LoadLostCurrency(GameData _data)
    {
        lostcurrncyX = _data.lostcurrncyX;
        lostcurrncyY = _data.lostcurrncyY;
        lostCurrencyAmount = _data.lostcurrncyAmount;
        if (lostCurrencyAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostCurrencyPrefab, new Vector3(lostcurrncyX, lostcurrncyY), Quaternion.identity);
            newLostCurrency.GetComponent<LostCurrencyController>().currency = lostCurrencyAmount;
        }
        lostCurrencyAmount=0;
    }

    private void LoadClosestCheckPoint(GameData _data)
    {
        checkPointClosestId = _data.checkPointClossest;
        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (checkPoint.Id == checkPointClosestId)
            {
                player.position = checkPoint.transform.position;
            }
        }
    }
    private IEnumerator LoadWithDelay(GameData _data){
        yield return new WaitForSeconds(.1f);
        LoadCheckPoint(_data);
        LoadClosestCheckPoint(_data);
        LoadLostCurrency(_data);

    }

    private CheckPoint FindCheckPointClosest(){
        float closestDistance=Mathf.Infinity;
        CheckPoint closestCheckPoint=null;
        foreach (var checkPoint in checkPoints)
        {
            float distanceToCheckPoint=Vector2.Distance(player.position,checkPoint.transform.position);
            if(distanceToCheckPoint<closestDistance&&checkPoint.actived==true){
                closestDistance=distanceToCheckPoint;
                closestCheckPoint=checkPoint;
            }
        }
        return closestCheckPoint;
    }
    public void PauseGame(bool _pause){
        if(_pause){
            Time.timeScale=0;
        }else{
            Time.timeScale=1;
        }
    }
}
