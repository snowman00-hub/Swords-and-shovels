using UnityEngine;

public class MonsterHealth : LivingEntity
{
    [SerializeField] private PlayerBehavior playerBehavior;

    private float monsterMaxHealth = 30f;

    protected override void OnEnable()
    {
        base.OnEnable();
        currentHp = monsterMaxHealth;
        gameObject.SetActive(true);
    }

    public override void Ondamage(float damage, Vector3 hitPosition)
    {
        if (isDead) return;

        base.Ondamage(damage, hitPosition);

        if (isDead && playerBehavior != null)
        {
            playerBehavior.OnMonsterDead();
        }
    }
}
