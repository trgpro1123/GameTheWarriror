using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    strength,
    agility,
    intelegence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicRes,
    fireDamage,
    iceDamage,
    lightingDamage
}
public class CharaterStats : MonoBehaviour
{

    [Header("Major Stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;
    
    [Header("Offensive Stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;
    [Header("Defensive Stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic Stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;


    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

    [SerializeField] private float alimentsDuration;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    private int ignitedDamage;
    private float ignitedCoolDown=0.3f;
    private float ignitedDamageTimer;


    [SerializeField] private GameObject shockStrikePrefab;
    private int shockDamage;
    public int currentHealth;


    public System.Action onHealthChanged;
    private EntityFX entityFX;

    // public System.Action onHealthChange;
    public bool isDead {get;private set;}

    private bool isVulnerable;
    private bool isInvinsable;
    protected HealthBar_UI healthBar_UI;


    private void Awake() {
        
    }
    protected virtual void Start() {
        critPower.SetDefaultValue(150);
        // currentHealth=maxHealth.GetValue();
        currentHealth=GetMaxHealth();
        entityFX=GetComponent<EntityFX>();
        healthBar_UI=GetComponentInChildren<HealthBar_UI>();
    }
    private void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        ignitedDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0) isIgnited = false;
        if (chilledTimer < 0) isChilled = false;
        if (shockedTimer < 0) isShocked = false;
        if(isIgnited)
            ApplyIgnitedDamage();
    }

    public virtual void OnEvasion(){

    }
    #region Stat caculations
    protected bool TargetCanAvoidAttack(CharaterStats _targetStat){
        int totalEvation=_targetStat.agility.GetValue()+_targetStat.evasion.GetValue();
        if(isShocked) totalEvation+=20;
        if(Random.Range(0,100)<totalEvation){
            _targetStat.OnEvasion();
            return true;
        }
        return false;
    }
    protected int CheckTargetArmor(CharaterStats _targetStat,int _totalDamage){
        _totalDamage-=_targetStat.armor.GetValue();
        if (isChilled) _totalDamage-=Mathf.RoundToInt(armor.GetValue()*0.8f);
        else _totalDamage-=armor.GetValue();
         _totalDamage=Mathf.Clamp(_totalDamage,0,int.MaxValue);
        return _totalDamage;
    }
    protected bool CanCrit(){
        int totalCritChance=critChance.GetValue()+agility.GetValue();
        if(Random.Range(0,100)<totalCritChance) return true;
        return false;
    }
    protected int CaculatorCriticalDamage(int _damage){
        float totalCritPower=(critPower.GetValue()+strength.GetValue())*.01f;
        float totalDamage=totalCritPower*_damage;
        return Mathf.RoundToInt(totalDamage);
    }
    private int CheckTargetMagicresistance(CharaterStats _targetStat, int totalMagicDamage)
    {
        totalMagicDamage -= magicResistance.GetValue() + (_targetStat.intelligence.GetValue() * 3);
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 0, int.MaxValue);
        return totalMagicDamage;
    }
    public int GetMaxHealth(){
        return maxHealth.GetValue()+vitality.GetValue()*5;
    }
    #endregion
    

    #region Magical Damage and Ailemnts
    public virtual void DoMagicDamage(CharaterStats _targetStat,float damage=1)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = lightingDamage.GetValue();
        int totalMagicDamage = Mathf.RoundToInt((_fireDamage + _iceDamage + _lightningDamage)*damage)+intelligence.GetValue()*3;
        totalMagicDamage = CheckTargetMagicresistance(_targetStat, totalMagicDamage);

        _targetStat.TakeDamage(totalMagicDamage);
        

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0) return;

        AttemptyToApplyAilements(_targetStat, _fireDamage, _iceDamage, _lightningDamage);

    }

    public virtual void DoDamage(CharaterStats _targetStat){

        bool isCrit=false;
        
        if(TargetCanAvoidAttack(_targetStat)){
            _targetStat.entityFX.CreatePopUpText("Dodge");
            return;
        }

        if(_targetStat.isInvinsable) return;

        _targetStat.GetComponent<Entity>().SetupKnockBackDir(transform);

        int totalDamage=damage.GetValue()+strength.GetValue();
        if(CanCrit()){
            isCrit=true;
            totalDamage=CaculatorCriticalDamage(totalDamage);
        }

        entityFX.CreateHitFX(_targetStat.transform,isCrit);
        totalDamage=CheckTargetArmor(_targetStat,totalDamage);
        _targetStat.TakeDamage(totalDamage);
        DoMagicDamage(_targetStat);

    }
    private static void AttemptyToApplyAilements(CharaterStats _targetStat, int _fireDamage, int _iceDamage, int _lightningDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool canApplyShock = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;
        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .3 && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStat.ApplyAliments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .4 && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStat.ApplyAliments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (Random.value < .5 && _lightningDamage > 0)
            {
                canApplyShock = true;
                _targetStat.ApplyAliments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }
        if (canApplyIgnite) _targetStat.SetUpIgnitedDamage(Mathf.RoundToInt(_fireDamage * 0.2f));
        if (canApplyShock) _targetStat.SetUpShockStrikeDamage(Mathf.RoundToInt(_lightningDamage * .2f));
        
        _targetStat.ApplyAliments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    

    private void ApplyAliments(bool _ignite,bool _chill,bool _shock){
        bool canApplyIgnite=!isIgnited && !isChilled && !isShocked;
        bool canApplyChill=!isIgnited && !isChilled && !isShocked;
        bool canApplyShock=!isIgnited && !isChilled;
        if(_ignite&&canApplyIgnite){
            isIgnited=_ignite;
            ignitedTimer=alimentsDuration;
            entityFX.IgniteFXFor(alimentsDuration);

        }
        if(_chill&&canApplyChill){
            isChilled=_chill;
            chilledTimer=alimentsDuration;
            float slowPercentage=0.2f;
            GetComponent<Entity>().SlowEtityBy(slowPercentage,alimentsDuration);
            entityFX.ChillFXFor(alimentsDuration);
        }
        if(_shock&&canApplyShock){

            if(!isShocked)
            {
                ApplyShock(_shock);

            }
            else
            {
                if (GetComponent<Player>() != null) return;
                HitNearestTargetWithShockStrike();
            }
        }

    }

    public void ApplyShock(bool _shocked)
    {
        if(isShocked) return;
        isShocked = _shocked;
        shockedTimer = 2f;
        entityFX.ShockFXFor(alimentsDuration);
    }

    private void HitNearestTargetWithShockStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;

                }
            }
            if (closestEnemy == null) closestEnemy = transform;
        }
        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);
            newShockStrike.GetComponent<ShockStrike_Controller>().SetUpShockStrike(shockDamage, closestEnemy.GetComponent<CharaterStats>());
        }
    }

    private void SetUpShockStrikeDamage(int _damage)=>shockDamage=_damage;
    public void SetUpIgnitedDamage(int _damage)=>ignitedDamage=_damage;
    #endregion

    public virtual void TakeDamage(int _damage){
        if(isInvinsable) return;
        DecreaseHealthBy(_damage);
        GetComponent<Entity>().DamageImpact();
        entityFX.StartCoroutine("FlashFX");
        if(currentHealth<=0 && !isDead) Die();

    }
    public void IncreaseStatBy(int _amountModifier,float _duration,Stat _statModifier){
        StartCoroutine(StatModCoroutine(_amountModifier,_duration,_statModifier));
    }
    private IEnumerator StatModCoroutine(int _amountModifier,float _duration,Stat _statModifier){
        _statModifier.AddModifier(_amountModifier);
        yield return new WaitForSeconds(_duration);
        _statModifier.RemoveModifier(_amountModifier);
    }
    public void IncreaseHealBy(int _heal){
        currentHealth+=_heal;
        if(currentHealth>GetMaxHealth()){
            currentHealth=GetMaxHealth();
        }
        if(onHealthChanged!=null) onHealthChanged();
    }
    public virtual void DecreaseHealthBy(int _damage){
        if(isVulnerable) _damage=Mathf.RoundToInt(_damage*1.15f);
        currentHealth-=_damage;
        if(_damage>0) entityFX.CreatePopUpText(_damage.ToString());

        if(onHealthChanged!=null) onHealthChanged();
    }
    public void MakeVulnareableFor(float _duration){
        StartCoroutine(VulnerableCoroutine(_duration));
    }
    private IEnumerator VulnerableCoroutine(float _duration){
        isVulnerable=true;
        yield return new WaitForSeconds(_duration);
        isVulnerable=false;
    }
    protected virtual void Die(){
        isDead=true;
    }
    public void KillYourSelf()=>Die();


    private void ApplyIgnitedDamage()
    {
        if (ignitedDamageTimer < 0 && isIgnited)
        {
            DecreaseHealthBy(ignitedDamage);
            if (currentHealth == 0) Die();
            ignitedDamageTimer = ignitedCoolDown;
        }
    }
    public Stat GetType(StatType _statType)
    {
        if (_statType == StatType.strength) return strength;
        else if (_statType == StatType.agility) return agility;
        else if (_statType == StatType.intelegence) return intelligence;
        else if (_statType == StatType.vitality) return vitality;
        else if (_statType == StatType.damage) return damage;
        else if (_statType == StatType.critChance) return critChance;
        else if (_statType == StatType.critPower) return critPower;
        else if (_statType == StatType.health) return maxHealth;
        else if (_statType == StatType.armor) return armor;
        else if (_statType == StatType.evasion) return evasion;
        else if (_statType == StatType.magicRes) return magicResistance;
        else if (_statType == StatType.fireDamage) return fireDamage;
        else if (_statType == StatType.iceDamage) return iceDamage;
        else if (_statType == StatType.lightingDamage) return lightingDamage;

        return null;
    }
    public void MakeInvinsable(bool _isInvisable)=> isInvinsable=_isInvisable;
    
}
