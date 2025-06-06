using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();

        GameManager.instance.lostCurrencyAmount = PlayerManager.instance.currency;
        PlayerManager.instance.currency = 0;

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }

    public override void DecreaseHealthBy(float _damage)
    {
        base.DecreaseHealthBy(_damage);

        if (_damage >= GetTotalMaxHealthValue() * .3f)
        {
            player.SetupKnockbackPower(new Vector2(7, 7));
            player.fx.ScreenShake(player.fx.shakeHighDamage);
            AudioManager.instance.PlayerSFX(33, null);
            Debug.Log("player is heavily knockbacked");
        }

        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

        if (currentArmor != null)
        {
            currentArmor.Effect(player.transform);
        }
    }

    public override void OnEvasion()
    {
        player.skill.dodge.CreateMirageOnDodge();
    }

    public void CloneDoDamage(CharacterStats _targetStats, float _multiplier)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        float totalDamage = GetTotalPhisicalDamgeValue();
        if (_multiplier > 0)
            totalDamage = totalDamage * _multiplier;
        totalDamage = Mathf.Round(totalDamage);

        if (canCrit(_targetStats))
        {
            totalDamage = CalculateCritDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);
    }
}
