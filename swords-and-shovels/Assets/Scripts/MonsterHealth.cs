using UnityEngine;

public class MonsterHealth : LivingEntity
{
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
        DamagePopupManager.Instance.ShowDamage(hitPosition, Mathf.FloorToInt(damage));
    }
}
