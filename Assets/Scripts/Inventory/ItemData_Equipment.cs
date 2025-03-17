using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType{
    Weapon,
    Armo,
    Amulet,
    Flask
}
[CreateAssetMenu(fileName ="New Item Data",menuName ="Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    public float itemCooldown;
    public ItemEffect[] itemEffects;


    [Header("Major stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    [Header("Craft Requirements")]
    public List<InventoryItem> craftingMaterials; 


    private int desciptionLength;
    

    public void AddModifiers(){
        PlayerStats playerStats=PlayerManage.instance.player.GetComponent<PlayerStats>();
        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }
    public void RemoveModifiers(){
        PlayerStats playerStats = PlayerManage.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);


        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);
        

        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);


        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }


    public void ItemEffect(Transform _target){
        foreach (var item in itemEffects)
        {
            item.ExcectEffect(_target);
        }
    }

    public override string GetDesciptrion()
    {
        sb.Length=0;
        desciptionLength=0;
        AddItemDescription(strength, "Strength");
        AddItemDescription(agility, "Agility");
        AddItemDescription(intelligence, "Intelligence");
        AddItemDescription(vitality, "Vitality");

        AddItemDescription(damage, "Damage");
        AddItemDescription(critChance, "Crit.Chance");
        AddItemDescription(critPower, "Crit.Power");

        AddItemDescription(health, "Health");
        AddItemDescription(evasion, "Evasion");
        AddItemDescription(armor, "Armor");
        AddItemDescription(magicResistance, "Magic Resist.");

        AddItemDescription(fireDamage, "Fire damage");
        AddItemDescription(iceDamage, "Ice damage");
        AddItemDescription(lightingDamage, "Lighting dmg. ");


        for (int i = 0; i < itemEffects.Length; i++)
        {
            if(itemEffects[i].itemEffectDescription.Length>0){
                sb.AppendLine();
                sb.Append("Unique: "+itemEffects[i].itemEffectDescription);
                desciptionLength++;
            }
        }

        if(desciptionLength<5){
            for (int i = 0; i < 5-desciptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }
        
        return sb.ToString();
    }
    public void AddItemDescription(int _value,string _name){
        if(_value!=0){
            if(sb.Length>0){
                sb.AppendLine();
            }
            if(_value>0){
                sb.Append("+"+_value+" "+_name);
            }
            desciptionLength++;
        }
    }

    
}
