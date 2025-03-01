using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop myDropSysItem;
    public Stats soulsDropAmount; 

    [Header("Level details")]
    [SerializeField] private int level = 1;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier = .4f;

    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(100);

        ApplyModifier();

        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSysItem = GetComponent<ItemDrop>();
    }

    private void ApplyModifier()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(damage);
        Modify(critChange);
        Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);
        
        Modify(soulsDropAmount);
    }

    private void Modify(Stats _stats)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = _stats.GetValue() * percentageModifier;

            _stats.AddModifier((int)modifier);
        }
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }
    protected override void Die()
    {
        base.Die();
        enemy.Die();

        PlayerManager.instance.currency += soulsDropAmount.GetValue();
        myDropSysItem.GenerateDrop();

        Destroy(gameObject,3f);
    }
}
