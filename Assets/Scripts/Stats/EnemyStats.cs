using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharaterStats
{
    public Stat soulDropAmount;
    [Header("Level Detail")]
    [SerializeField] private int level;

    [Range(0,1)] 
    [SerializeField] private float percantagedModifiers;


    private ItemDrop itemDrop;
    private Enemy enemy;


    protected override void Start()
    {
        //soulDropAmount.SetDefaultValue(100);
        ApplyLevelModifiers();
        base.Start();
        enemy = GetComponent<Enemy>();
        itemDrop=GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);
        Modify(soulDropAmount);
    }

    public override void TakeDamage(int _damge)
    {
        base.TakeDamage(_damge);

    }
    public void Modify(Stat _stat){
        for (int i = 0; i < level; i++)
        {
            float modifier=_stat.GetValue()*percantagedModifiers;
            _stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();
        PlayerManage.instance.currency+=soulDropAmount.GetValue();
        Destroy(gameObject,5);
        itemDrop.GenerateDropItem();
    }
}
