using UnityEngine;

public class MonsterHealth : LivingEntity
{
    private float monsterMaxHealth = 30f;
    [SerializeField]public int monsterDef = 5;

    protected override void OnEnable()
    {
        base.OnEnable();
        currentHp = monsterMaxHealth;
        def = monsterDef;
        gameObject.SetActive(true);
    }

    public override void OnDamage(float damage, Vector3 hitPosition)
    {
        if (isDead) return;

        base.OnDamage(damage, hitPosition);
    }
}
