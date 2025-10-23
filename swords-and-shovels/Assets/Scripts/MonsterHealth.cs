using UnityEngine;

public class MonsterHealth : LivingEntity
{
    [SerializeField] private PlayerBehavior playerBehavior;

    private float monsterMaxHealth = 30f;

    protected override void OnEnable()
    {
        base.OnEnable();
        currentHp = monsterMaxHealth;

        if (playerBehavior == null)
        {
            playerBehavior = FindObjectOfType<PlayerBehavior>();
        }
        gameObject.SetActive(true);
    }

    public override void OnDamage(float damage, Vector3 hitPosition)
    {
        if (isDead) return;

        base.OnDamage(damage, hitPosition);
        DamagePopupManager.Instance.ShowDamage(hitPosition, Mathf.FloorToInt(damage));

        if (isDead && playerBehavior != null)
        {
            playerBehavior.OnMonsterDead();
        }
    }
}
