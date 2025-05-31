using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]

public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Item Cooldown")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;

    [Header("Major Stats")]
    public float strengeth;
    public float agility;
    public float intelligence;
    public float vitality;

    [Header("Offensive Stats")]
    public float phsicalDamage;
    public float critChance;
    public float critPower;

    [Header("Defensive Stats")]
    public float maxHealth;
    public float armor;
    public float evasion;
    public float magicResistance;

    [Header("Magic Stats")]
    public float fireDamge;
    public float iceDamage;
    public float shockDamage;

    [Header("Craft Requirements")]
    public List<InventoryItem> craftingMaterials;

    private int descriptionLength;

    public void Effect(Transform _executeTransform)
    {
        foreach (var effect in itemEffects)
        {
            effect.ExecuteEffect(_executeTransform);
        }
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strengeth.AddModifier(strengeth);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.phsicalDamage.AddModifier(phsicalDamage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.fireDamage.AddModifier(fireDamge);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.shockDamage.AddModifier(shockDamage);
    }

    public void RemoveModifiers() {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strengeth.RemoveModifier(strengeth);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.phsicalDamage.RemoveModifier(phsicalDamage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.fireDamage.RemoveModifier(fireDamge);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.shockDamage.RemoveModifier(shockDamage);
    }

    public override string GetDiscription()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(strengeth, "Á¦Á¿");
        AddItemDescription(agility, "Ãô½Ý");
        AddItemDescription(intelligence, "ÖÇÁ¦");
        AddItemDescription(vitality, "¾«Á¦");

        AddItemDescription(phsicalDamage, "Îï¹¥");
        AddItemDescription(critChance, "±©»÷");
        AddItemDescription(critPower, "±©ÉË");

        AddItemDescription(maxHealth, "ÉúÃü");
        AddItemDescription(armor, "»¤¼×");
        AddItemDescription(evasion, "ÉÁ±Ü");
        AddItemDescription(magicResistance, "Ä§¿¹");

        AddItemDescription(fireDamge, "»ð¸½Ä§");
        AddItemDescription(iceDamage, "±ù¸½Ä§");
        AddItemDescription(shockDamage, "À×¸½Ä§");

        sb.AppendLine();
        descriptionLength++;
        for (int i = 0; i < itemEffects.Length; i++)
        {
            if (itemEffects[i].effectDescription.Length > 0)
            {
                sb.AppendLine();
                sb.Append("Ð§¹û: " + itemEffects[i].effectDescription);
                descriptionLength++;
            }
        }

        if (descriptionLength < 5)
        {
            for (int i = 0; i < 5 - descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        return sb.ToString();
    }

    public string GetEquipmentType()
    {
        string et = "ÎäÆ÷";
        if (equipmentType == EquipmentType.Amulet)
            et = "ÊÎÆ·";
        else if (equipmentType == EquipmentType.Armor)
            et = "»¤¼×";
        else if (equipmentType == EquipmentType.Flask)
            et = "Ò©¼Á";
        return et;
    }
    private void AddItemDescription(float _value, string _name)
    {
        if (_value != 0) 
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
                descriptionLength++;
            }
            if(_value > 0)
            {
                sb.Append( "+ " + _value + " " + _name);
            }
        }
    }
}
