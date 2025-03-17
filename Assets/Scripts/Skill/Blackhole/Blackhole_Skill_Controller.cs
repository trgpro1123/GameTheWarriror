using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotkeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList=new List<KeyCode>();
    
    private bool canCreateHotKey=true;
    public bool canExitState {get;private set;}
    private float blackholeTimer;
    private bool canDisapear=true;
    
    
    private bool canGrow=true;
    private float growSpeed;
    private float maxSize;


    private bool canShirnk;
    private float shinkSpeed;

    private float attackCloneTimer;
    private float attackCloneCooldown=0.3f;
    private float amountCloneAttack;
    private bool attackCloneRelease;

    
    private List<GameObject> createHotKey=new List<GameObject>();
    private List<Transform> enemyList=new List<Transform>();

    private void Update()
    {
        attackCloneTimer-=Time.deltaTime;
        blackholeTimer-=Time.deltaTime;
        if(blackholeTimer<=0){
            blackholeTimer=Mathf.Infinity;
            if(enemyList.Count>0) ReleaseCloneAttack();
            else FinishBlackholeAbility();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ReleaseCloneAttack();
        }


        if (attackCloneRelease&&attackCloneTimer<0&&amountCloneAttack>0){
            attackCloneTimer=attackCloneCooldown;
            int ranomIndex=Random.Range(0,enemyList.Count);
            float xOffset;
            if(Random.Range(0,100)>50) xOffset=2;
            else xOffset=-2;
            if(SkillManager.instance.clone.crystalInsteadOfClone){
                SkillManager.instance.crystal.CreateCrystal();
                SkillManager.instance.crystal.CurrentCrystalChooseRandomEnemy();
            }
            else{

                SkillManager.instance.clone.CreateClone(enemyList[ranomIndex],new Vector3(xOffset,0));
            }
            amountCloneAttack--;
            if(amountCloneAttack<=0)
            {
                Invoke("FinishBlackholeAbility",0.7f);
            }


        }
        if(canGrow&&!canShirnk){
            transform.localScale=Vector2.Lerp(transform.localScale,new Vector2(maxSize,maxSize),growSpeed*Time.deltaTime);

        }

        if(canShirnk){
            transform.localScale=Vector2.Lerp(transform.localScale,new Vector2(-1,-1),shinkSpeed*Time.deltaTime);
            if(transform.localScale.x<0) Destroy(gameObject);
        }    
    }

    private void ReleaseCloneAttack()
    {
        if(enemyList.Count<=0) return;
        attackCloneRelease = true;
        canCreateHotKey = false;
        DestroyHotKey();
        if(canDisapear){
            PlayerManage.instance.player.fX.MakeTransprent(true);
            canDisapear=false;
        }
    }

    private void FinishBlackholeAbility()
    {
        PlayerManage.instance.player.fX.MakeTransprent(false);
        DestroyHotKey();
        canExitState=true;
        canShirnk = true;
        attackCloneRelease = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Enemy>()!=null)
        {
            other.GetComponent<Enemy>().FreezeTime(true);
            CreateHotkey(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<Enemy>()!=null)
        {
            other.GetComponent<Enemy>().FreezeTime(false);
        }
    }
    private void DestroyHotKey(){
        if(createHotKey.Count<=0) return;
        for (int i = 0; i < createHotKey.Count; i++)
        {
            Destroy(createHotKey[i]);
        }
    }
    private void CreateHotkey(Collider2D other)
    {
        if(keyCodeList.Count<=0) return;
        if(attackCloneRelease) return;
        if(!canCreateHotKey) return;
        GameObject newHotkey = Instantiate(hotkeyPrefab, other.transform.position + new Vector3(0, 2), Quaternion.identity);
        createHotKey.Add(newHotkey);
        KeyCode choosekey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosekey);
        Blackhole_Hotkey_Controller hotkey_Controller = newHotkey.GetComponent<Blackhole_Hotkey_Controller>();
        hotkey_Controller.SetUpHotkey(choosekey, other.transform, this);
    }

    public void AddEnemyToList(Transform _enemy)=>enemyList.Add(_enemy);
    public void SetUpBlackholeSkill(float _maxSize,float _growSpeed,float _shinkSpeed,int _amountCloneAttack,float _attackCloneCooldown,float _blackholeDuration){
        maxSize=_maxSize;
        growSpeed=_growSpeed;
        shinkSpeed=_shinkSpeed;
        amountCloneAttack=_amountCloneAttack;
        attackCloneCooldown=_attackCloneCooldown;
        blackholeTimer=_blackholeDuration;
        if(SkillManager.instance.clone.crystalInsteadOfClone)
            canDisapear=false;
    }
    


}
