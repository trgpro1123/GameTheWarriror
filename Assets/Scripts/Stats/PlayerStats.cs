using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharaterStats
{
    private Player player;
    private PlayerItemDrop playerItemDrop;
    
    protected override void Start() {
        base.Start();
        player=GetComponent<Player>();
        playerItemDrop=GetComponent<PlayerItemDrop>();
        
        Invoke("GetHealth",.05f);
        // GetHealth();
    }
    private void GetHealth(){
        currentHealth=GetMaxHealth();
        healthBar_UI.UpdateHealth();
    }
    public override void TakeDamage(int _damge)
    {
        base.TakeDamage(_damge);

    }
    protected override void Die()
    {
        base.Die();
        player.Die();
        GameManager.instance.lostCurrencyAmount=PlayerManage.instance.GetCurrency();
        PlayerManage.instance.currency=0;
        playerItemDrop.GenerateDropItem();
        
    }
    public override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);
        if(_damage>GetMaxHealth()*.3f){
            player.fX.ScreenShake(player.fX.shakeHighDamaged);
            player.SetupKockBackPower(new Vector2(10,10));
        }
        ItemData_Equipment currentArmo=Inventory.instance.GetEquiment(EquipmentType.Armo);
        if(currentArmo!=null) currentArmo.ItemEffect(player.transform);
    }
    public override void OnEvasion()
    {
        player.skill.dodge.CreateMirageOnDodge();
    }
    public void DoDamageClone(CharaterStats _targetStat,float _mutiple){
        if(TargetCanAvoidAttack(_targetStat)) return;
        int totalDamage=damage.GetValue()+strength.GetValue();
        if(_mutiple>0)
            totalDamage=Mathf.RoundToInt(totalDamage*_mutiple);

        if(CanCrit()) totalDamage=CaculatorCriticalDamage(totalDamage);
        totalDamage=CheckTargetArmor(_targetStat,totalDamage);

        _targetStat.TakeDamage(totalDamage);

    }
    
}
