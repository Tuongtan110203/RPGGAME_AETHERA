using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        if (isDead) return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        base.Die();

        player.Die();


        GameManager.instance.lostCurrencyAmount = PlayerManager.instance.currency;

        PlayerManager.instance.currency = 0;

        GetComponent<PlayerItemDrop>()?.GenerateDrop();
    }


    public override void DescreaseHealthBy(int _damage)
    {
        base.DescreaseHealthBy(_damage);

        if(_damage > GetMaxValueHP() * .3f)
        {
            player.SetupKnockbackPower(new Vector2(12, 5));
            player.fx.ScreenShake(player.fx.shakeHighDamage);
            int randomSound = Random.Range(34, 35);

            AudioManager.instance.PlaySFX(randomSound, null);

        }

        ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

        if (currentArmor != null)
        {
            currentArmor.Effect(player.transform);
        }
    }

    public override void OnEvasion()
    {
        player.skill.dodgeSkill.CreateMirageOnDodge();

       
    }

    public void CloneDoDamage(CharacterStats _stats, float _multiplier)
    {
        if (TargetCanAvoidAttack(_stats))
        {
            return;
        }

        int totalDamage = damage.GetValue() + strength.GetValue();


        if (_multiplier > 0)
        {
            totalDamage *= (int)_multiplier;
        }


        if (canCrit())
        {
            totalDamage = CalculateCriticalDame(totalDamage);
        }

        totalDamage = CheckTargetArmor(_stats, totalDamage);

        _stats.TakeDamage(totalDamage);

        //DoMagicalDamage(_stats);  //remove if you dont't want to apply magic hit on primary attack
    }

}
